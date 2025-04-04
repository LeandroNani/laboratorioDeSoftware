"use client";

import React, { useEffect, useState } from "react";

type Cliente = {
  nome: string;
  cpf: string;
  endereco: string;
  profissao: string;
};

type Automovel = {
  id: number;
  marca: string;
  modelo: string;
  ano: number;
  placa: string;
};

type PedidoDTO = {
  id: number;
  status: string;
  duracao: number;
  tipoContrato: string;
  marcaCarro: string;
  modeloCarro: string;
};

export default function ClientePage() {
  const [selectedTab, setSelectedTab] = useState("perfil");
  const [cliente, setCliente] = useState<Cliente | null>(null);
  const [automoveis, setAutomoveis] = useState<Automovel[]>([]);
  const [pedidos, setPedidos] = useState<PedidoDTO[]>([]);
  const [contratos, setContratos] = useState<PedidoDTO[]>([]);
  const [showForm, setShowForm] = useState(false);
  const [pedidoForm, setPedidoForm] = useState({
    automovelId: 0,
    duracao: 0,
    tipoContrato: "",
  });

  const clienteId =
    typeof window !== "undefined" ? localStorage.getItem("usuarioId") : null;
  const token =
    typeof window !== "undefined" ? localStorage.getItem("token") : null;

  useEffect(() => {
    if (clienteId && token) {
      fetchCliente();
      fetchAutomoveis();
      fetchPedidos();
      fetchContratos();
    }
  }, [clienteId, token]);

  const fetchCliente = async () => {
    const response = await fetch(
      `http://localhost:5145/api/clientes/${clienteId}`,
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    const data: Cliente = await response.json();
    setCliente(data);
  };

  const fetchAutomoveis = async () => {
    const response = await fetch(
      "http://localhost:5145/api/automoveis/disponiveis",
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    const data: Automovel[] = await response.json();
    setAutomoveis(data);
  };

  const fetchPedidos = async () => {
    const response = await fetch(
      `http://localhost:5145/api/pedidos/minhas-solicitacoes?clienteId=${clienteId}`,
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    const data: PedidoDTO[] = await response.json();
    setPedidos(data);
  };

  const fetchContratos = async () => {
    const response = await fetch(
      `http://localhost:5145/api/pedidos/minhas-solicitacoes?clienteId=${clienteId}`,
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    const data: PedidoDTO[] = await response.json();
    const contratosAtivos = data.filter((p) => p.status === "aprovado");
    setContratos(contratosAtivos);
  };

  const handleSubmitPedido = async (e: React.FormEvent) => {
    e.preventDefault();

    const payload = {
      clienteId: parseInt(clienteId!),
      automovelId: pedidoForm.automovelId,
      duracao: pedidoForm.duracao,
      tipoContrato: pedidoForm.tipoContrato,
    };

    try {
      const response = await fetch("http://localhost:5145/api/pedidos", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
      });

      if (response.ok) {
        alert("Pedido criado com sucesso!");
        setShowForm(false);
        fetchPedidos();
      } else {
        const err = await response.text();
        alert("Erro ao criar pedido: " + err);
      }
    } catch (err) {
      alert("Erro de rede.");
      console.error(err);
    }
  };

  return (
    <div className="flex min-h-screen bg-gray-100 text-black">
      {/* Sidebar */}
      <aside className="w-72 bg-gray-800 shadow-lg min-h-screen p-6 flex flex-col text-white">
        <nav className="mt-6">
          {["perfil", "pedidos", "automoveis", "contratos"].map((tab) => (
            <button
              key={tab}
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${
                selectedTab === tab
                  ? "bg-white text-black font-semibold"
                  : "hover:bg-gray-700"
              }`}
              onClick={() => setSelectedTab(tab)}
            >
              {tab === "perfil" && "👤 Perfil"}
              {tab === "pedidos" && "📋 Pedidos de Aluguel"}
              {tab === "automoveis" && "🚗 Automóveis Disponíveis"}
              {tab === "contratos" && "✅ Contratos Ativos"}
            </button>
          ))}
        </nav>
      </aside>

      {/* Conteúdo principal */}
      <main className="flex-1 p-10">
        {/* Perfil */}
        {selectedTab === "perfil" && cliente && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Perfil</h2>
            <p className="text-lg text-gray-600">
              <strong>Nome:</strong> {cliente.nome}
            </p>
            <p className="text-lg text-gray-600">
              <strong>CPF:</strong> {cliente.cpf}
            </p>
            <p className="text-lg text-gray-600">
              <strong>Endereço:</strong> {cliente.endereco}
            </p>
            <p className="text-lg text-gray-600">
              <strong>Profissão:</strong> {cliente.profissao}
            </p>
          </div>
        )}

        {/* Pedidos */}
        {selectedTab === "pedidos" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-4xl mx-auto">
            <div className="flex justify-between items-center mb-6">
              <h2 className="text-xl font-semibold">Pedidos de Aluguel</h2>
              <button
                onClick={() => setShowForm(!showForm)}
                className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded"
              >
                {showForm ? "⬅ Voltar" : "+ Novo Pedido"}
              </button>
            </div>

            {showForm ? (
              <form className="space-y-4" onSubmit={handleSubmitPedido}>
                <div>
                  <label className="block mb-1 font-medium text-gray-700">
                    Automóvel
                  </label>
                  <select
                    value={pedidoForm.automovelId}
                    onChange={(e) =>
                      setPedidoForm({
                        ...pedidoForm,
                        automovelId: Number(e.target.value),
                      })
                    }
                    className="w-full border px-3 py-2 rounded"
                  >
                    <option value="">Selecione um automóvel</option>
                    {automoveis.map((a) => (
                      <option key={a.id} value={a.id}>
                        {a.marca} {a.modelo}
                      </option>
                    ))}
                  </select>
                </div>

                <div>
                  <label className="block mb-1 font-medium text-gray-700">
                    Duração (dias)
                  </label>
                  <input
                    type="number"
                    value={pedidoForm.duracao}
                    onChange={(e) =>
                      setPedidoForm({
                        ...pedidoForm,
                        duracao: Number(e.target.value),
                      })
                    }
                    className="w-full border px-3 py-2 rounded"
                  />
                </div>

                <div>
                  <label className="block mb-1 font-medium text-gray-700">
                    Tipo de Contrato
                  </label>
                  <select
                    value={pedidoForm.tipoContrato}
                    onChange={(e) =>
                      setPedidoForm({
                        ...pedidoForm,
                        tipoContrato: e.target.value,
                      })
                    }
                    className="w-full border px-3 py-2 rounded"
                  >
                    <option value="">Selecione o tipo</option>
                    <option value="credito">Crédito</option>
                    <option value="debito">Débito</option>
                  </select>
                </div>

                <button
                  type="submit"
                  className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded"
                >
                  Enviar Pedido
                </button>
              </form>
            ) : (
              <ul className="space-y-4">
                {pedidos.map((p) => (
                  <li key={p.id} className="border p-4 bg-gray-50 rounded">
                    <p><strong>Status:</strong> {p.status}</p>
                    <p><strong>Carro:</strong> {p.marcaCarro} {p.modeloCarro}</p>
                    <p><strong>Duração:</strong> {p.duracao} dias</p>
                    <p><strong>Contrato:</strong> {p.tipoContrato}</p>
                  </li>
                ))}
              </ul>
            )}
          </div>
        )}

        {/* Automóveis */}
        {selectedTab === "automoveis" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Automóveis Disponíveis</h2>
            <ul className="space-y-4">
              {automoveis.map((a) => (
                <li key={a.id} className="border p-4 bg-gray-50 rounded">
                  <p><strong>{a.marca} {a.modelo}</strong></p>
                  <p>Ano: {a.ano}</p>
                  <p>Placa: {a.placa}</p>
                </li>
              ))}
            </ul>
          </div>
        )}

        {/* Contratos */}
        {selectedTab === "contratos" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Contratos Ativos</h2>
            <ul className="space-y-4">
              {contratos.map((c) => (
                <li key={c.id} className="border p-4 bg-gray-50 rounded">
                  <p className="text-lg font-semibold">Contrato #{c.id}</p>
                  <p><strong>Automóvel:</strong> {c.marcaCarro} {c.modeloCarro}</p>
                </li>
              ))}
            </ul>
          </div>
        )}
      </main>
    </div>
  );
}

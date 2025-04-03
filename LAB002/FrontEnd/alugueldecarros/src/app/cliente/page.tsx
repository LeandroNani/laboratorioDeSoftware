"use client";

import React, { useState, useEffect } from "react";

type Automovel = {
  matricula: number;
  ano: number;
  marca: string;
  modelo: string;
  placa: string;
  quantidade: number;
};

type Pedido = {
  id: number;
  contratante: Cliente;
  agenteDesignado: Agente;
  status: boolean | null; // true = aprovado, false = negado, null = pendente
  automovel: Automovel;
  duracao: number;
  tipoContrato: string;
};

type Cliente = {
  rg: string;
  cpf: string;
  nome: string;
  endereco: string;
  profissao: string;
};

type Agente = {
  cnpj: string;
  nome: string;
  endereco: string;
  quantidadeCarros: number;
};

export default function ClientePage() {
  const [selectedTab, setSelectedTab] = useState("perfil");
  const [cliente, setCliente] = useState<Cliente | null>(null);
  const [automoveis, setAutomoveis] = useState<Automovel[]>([]);
  const [pedidos, setPedidos] = useState<Pedido[]>([]);
  const [contratos, setContratos] = useState<Pedido[]>([]);

  const [showForm, setShowForm] = useState(false);
  const [pedidoForm, setPedidoForm] = useState({
    automovelId: 0,
    duracao: 0,
    tipoContrato: "",
  });

  useEffect(() => {
    fetchCliente();
    fetchAutomoveis();
    fetchPedidos();
    fetchContratos();
  }, []);

  const fetchCliente = async () => {
    const mockCliente: Cliente = {
      rg: "11223344",
      cpf: "111.222.333-44",
      nome: "João da Silva",
      endereco: "Rua das Flores, 123",
      profissao: "Engenheiro",
    };
    setCliente(mockCliente);
  };

  const fetchAutomoveis = async () => {
    const mockAutomoveis: Automovel[] = [
      {
        matricula: 101,
        ano: 2020,
        marca: "Toyota",
        modelo: "Corolla",
        placa: "XYZ-1234",
        quantidade: 5,
      },
      {
        matricula: 102,
        ano: 2019,
        marca: "Honda",
        modelo: "Civic",
        placa: "XYZ-5678",
        quantidade: 3,
      },
    ];  
    setAutomoveis(mockAutomoveis);
  };

  const fetchPedidos = async () => {
    const mockPedidos: Pedido[] = [
      {
        id: 1,
        contratante: cliente!,
        agenteDesignado: {
          cnpj: "12.345.678/0001-99",
          nome: "Banco ABC",
          endereco: "Av. Central, 456",
          quantidadeCarros: 10,
        },
        status: null,
        duracao: 7,
        tipoContrato: "credito",
        automovel: mockAutomoveis[0],
      },
    ];
    setPedidos(mockPedidos);
  };

  const fetchContratos = async () => {
    const mockContratos: Pedido[] = [
      {
        id: 2,
        contratante: cliente!,
        agenteDesignado: {
          cnpj: "98.765.432/0001-00",
          nome: "Banco XYZ",
          endereco: "Av. Norte, 789",
          quantidadeCarros: 15,
        },
        status: true,
        duracao: 10,
        tipoContrato: "debito",
        automovel: mockAutomoveis[1],
      },
    ];
    setContratos(mockContratos);
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
        {selectedTab === "perfil" && cliente && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Perfil</h2>
            <p className="text-lg text-gray-600"><strong>Nome:</strong> {cliente.nome}</p>
            <p className="text-lg text-gray-600"><strong>CPF:</strong> {cliente.cpf}</p>
            <p className="text-lg text-gray-600"><strong>Endereço:</strong> {cliente.endereco}</p>
            <p className="text-lg text-gray-600"><strong>Profissão:</strong> {cliente.profissao}</p>
          </div>
        )}

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
              <form className="space-y-4">
                <div>
                  <label className="block mb-1 font-medium text-gray-700">Automóvel</label>
                  <select
                    value={pedidoForm.automovelId}
                    onChange={(e) =>
                      setPedidoForm({ ...pedidoForm, automovelId: Number(e.target.value) })
                    }
                    className="w-full border px-3 py-2 rounded"
                  >
                    <option value="">Selecione um automóvel</option>
                    {automoveis.map((a) => (
                      <option key={a.matricula} value={a.matricula}>
                        {a.marca} {a.modelo} ({a.placa})
                      </option>
                    ))}
                  </select>
                </div>

                <div>
                  <label className="block mb-1 font-medium text-gray-700">Duração (dias)</label>
                  <input
                    type="number"
                    value={pedidoForm.duracao}
                    onChange={(e) =>
                      setPedidoForm({ ...pedidoForm, duracao: Number(e.target.value) })
                    }
                    className="w-full border px-3 py-2 rounded"
                  />
                </div>

                <div>
                  <label className="block mb-1 font-medium text-gray-700">Tipo de Contrato</label>
                  <select
                    value={pedidoForm.tipoContrato}
                    onChange={(e) =>
                      setPedidoForm({ ...pedidoForm, tipoContrato: e.target.value })
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
                {pedidos.map((pedido) => (
                  <li key={pedido.id} className="border border-gray-300 rounded-lg p-4 bg-gray-50 shadow-sm">
                    <p className="text-lg font-semibold">Pedido #{pedido.id}</p>
                    <p className="text-sm text-gray-600">
                      <strong>Status:</strong>{" "}
                      {pedido.status === null
                        ? "Pendente"
                        : pedido.status
                        ? "Aprovado ✅"
                        : "Negado ❌"}
                    </p>
                    <p className="text-sm text-gray-600">
                      <strong>Automóvel:</strong> {pedido.automovel.marca} {pedido.automovel.modelo}
                    </p>
                    <p className="text-sm text-gray-600">
                      <strong>Duração:</strong> {pedido.duracao} dias
                    </p>
                    <p className="text-sm text-gray-600">
                      <strong>Tipo de Contrato:</strong> {pedido.tipoContrato}
                    </p>
                    <div className="mt-4 flex gap-3">
  <button className="px-4 py-1 bg-blue-500 text-white rounded hover:bg-blue-600 transition">
    Editar
  </button>
  <button className="px-4 py-1 bg-red-500 text-white rounded hover:bg-red-600 transition">
    Cancelar
  </button>
</div>
                  </li>
                ))}
              </ul>
            )}
          </div>
        )}

        {selectedTab === "automoveis" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Automóveis Disponíveis</h2>
            <ul className="space-y-4">
              {automoveis.map((a) => (
                <li key={a.matricula} className="border border-gray-300 rounded-lg p-4 bg-gray-50 shadow-sm">
                  <p className="text-lg font-semibold">{a.marca} {a.modelo}</p>
                  <p className="text-sm text-gray-600"><strong>Ano:</strong> {a.ano}</p>
                  <p className="text-sm text-gray-600"><strong>Placa:</strong> {a.placa}</p>
                </li>
              ))}
            </ul>
          </div>
        )}

        {selectedTab === "contratos" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Contratos Ativos</h2>
            <ul className="space-y-4">
              {contratos.map((c) => (
                <li key={c.id} className="border border-gray-300 rounded-lg p-4 bg-gray-50 shadow-sm">
                  <p className="text-lg font-semibold">Contrato #{c.id}</p>
                  <p className="text-sm text-gray-600"><strong>Automóvel:</strong> {c.automovel.marca} {c.automovel.modelo}</p>
                </li>
              ))}
            </ul>
          </div>
        )}
      </main>
    </div>
  );
}

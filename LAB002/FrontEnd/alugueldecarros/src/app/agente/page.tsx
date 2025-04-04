"use client";

import React, { useState, useEffect } from "react";

type Rendimento = {
  valor: number;
  fonte: string;
};

type PedidoPendente = {
  pedidoId: number;
  nomeCliente: string;
  cpfCliente: string;
  marcaCarro: string;
  modeloCarro: string;
  fotoCarro?: string;
};

type DetalhePedido = {
  pedidoId: number;
  status: string;
  nomeCliente: string;
  cpfCliente: string;
  profissaoCliente: string;
  rendimentos: Rendimento[];
  marcaCarro: string;
  modeloCarro: string;
  placaCarro: string;
  anoCarro: number;
  duracao: number;
  tipoContrato: string;
};

type Automovel = {
  id: number;
  matricula: number;
  ano: number;
  marca: string;
  modelo: string;
  placa: string;
};

export default function AgentePage() {
  const [selectedTab, setSelectedTab] = useState("pedidos");
  const [pedidos, setPedidos] = useState<DetalhePedido[]>([]);
  const [automoveis, setAutomoveis] = useState<Automovel[]>([]);

  useEffect(() => {
    fetchPedidos();
    fetchAutomoveis();
  }, []);

  const fetchPedidos = async () => {
    const token = localStorage.getItem("token");
    try {
      const res = await fetch("http://localhost:5145/api/agente/pedidos-pendentes", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      const pedidosRes: PedidoPendente[] = await res.json();

      // Buscar detalhes de cada pedido
      const detalhes: DetalhePedido[] = [];
      for (const p of pedidosRes) {
        const res = await fetch(`http://localhost:5145/api/agente/pedidos/${p.pedidoId}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        const detalhe = await res.json();
        detalhes.push(detalhe);
      }

      setPedidos(detalhes);
    } catch (error) {
      console.error("Erro ao buscar pedidos pendentes:", error);
    }
  };

  const fetchAutomoveis = async () => {
    const token = localStorage.getItem("token");
    try {
      const res = await fetch("http://localhost:5145/api/automoveis/meus-automoveis", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      const data = await res.json();
      setAutomoveis(data);
    } catch (error) {
      console.error("Erro ao buscar automóveis:", error);
    }
  };

  const handleAprovar = async (id: number) => {
    const token = localStorage.getItem("token");
    try {
      const res = await fetch(`http://localhost:5145/api/agente/pedidos/${id}/aprovar`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (res.ok) {
        alert(`Pedido #${id} aprovado!`);
        fetchPedidos();
      }
    } catch (err) {
      console.error("Erro ao aprovar pedido:", err);
    }
  };

  const handleNegar = async (id: number) => {
    const token = localStorage.getItem("token");
    try {
      const res = await fetch(`http://localhost:5145/api/agente/pedidos/${id}/negar`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (res.ok) {
        alert(`Pedido #${id} negado.`);
        fetchPedidos();
      }
    } catch (err) {
      console.error("Erro ao negar pedido:", err);
    }
  };

  return (
    <div className="flex min-h-screen bg-gray-100 text-black">
      {/* Sidebar */}
      <aside className="w-72 bg-gray-800 shadow-lg min-h-screen p-6 text-white">
        <nav className="mt-6 flex flex-col gap-2">
          <button
            className={`p-3 rounded-lg text-left text-lg transition ${
              selectedTab === "pedidos"
                ? "bg-white text-black font-semibold"
                : "hover:bg-gray-700"
            }`}
            onClick={() => setSelectedTab("pedidos")}
          >
            📋 Pedidos Recebidos
          </button>
          <button
            className={`p-3 rounded-lg text-left text-lg transition ${
              selectedTab === "automoveis"
                ? "bg-white text-black font-semibold"
                : "hover:bg-gray-700"
            }`}
            onClick={() => setSelectedTab("automoveis")}
          >
            🚗 Meus Automóveis
          </button>
        </nav>
      </aside>

      {/* Conteúdo */}
      <main className="flex-1 p-10">
        {selectedTab === "pedidos" && (
          <div className="bg-white rounded-lg shadow-lg p-6 max-w-5xl mx-auto">
            <h2 className="text-2xl font-bold mb-6">Pedidos de Aluguel</h2>
            <ul className="space-y-6">
              {pedidos.map((p) => (
                <li key={p.pedidoId} className="bg-gray-50 p-5 border rounded shadow-sm">
                  <div className="mb-2">
                    <p className="text-lg font-semibold">Pedido #{p.pedidoId}</p>
                    <p>Status: <strong>{p.status.toUpperCase()}</strong></p>
                    <p>Duração: {p.duracao} dias</p>
                    <p>Tipo de Contrato: {p.tipoContrato}</p>
                  </div>
                  <div className="mt-4">
                    <p className="font-medium text-gray-700">🚹 Cliente:</p>
                    <p>Nome: {p.nomeCliente}</p>
                    <p>CPF: {p.cpfCliente}</p>
                    <p>Profissão: {p.profissaoCliente}</p>
                    <p className="mt-1">Rendimentos:</p>
                    <ul className="ml-4 list-disc text-sm text-gray-600">
                      {p.rendimentos.map((r, idx) => (
                        <li key={idx}>{r.fonte}: R$ {r.valor.toFixed(2)}</li>
                      ))}
                    </ul>
                  </div>
                  <div className="mt-4">
                    <p className="font-medium text-gray-700">🚗 Automóvel:</p>
                    <p>{p.marcaCarro} {p.modeloCarro} - Placa: {p.placaCarro}</p>
                  </div>
                  <div className="mt-4 flex gap-4">
                    <button
                      className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
                      onClick={() => handleAprovar(p.pedidoId)}
                    >
                      ✅ Aprovar
                    </button>
                    <button
                      className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700"
                      onClick={() => handleNegar(p.pedidoId)}
                    >
                      ❌ Negar
                    </button>
                  </div>
                </li>
              ))}
            </ul>
          </div>
        )}

        {selectedTab === "automoveis" && (
          <div className="bg-white rounded-lg shadow-lg p-6 max-w-4xl mx-auto">
            <h2 className="text-2xl font-bold mb-4">Automóveis Vinculados</h2>
            <ul className="space-y-4">
              {automoveis.map((auto) => (
                <li key={auto.id} className="border bg-gray-50 rounded p-4 shadow-sm">
                  <p className="font-semibold text-lg">{auto.marca} {auto.modelo}</p>
                  <p>Matrícula: {auto.matricula}</p>
                  <p>Ano: {auto.ano}</p>
                  <p>Placa: {auto.placa}</p>
                </li>
              ))}
            </ul>
          </div>
        )}
      </main>
    </div>
  );
}

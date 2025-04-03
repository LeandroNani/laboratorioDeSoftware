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

  useEffect(() => {
    fetchCliente();
    fetchAutomoveis();
    fetchPedidos();
    fetchContratos();
  }, []);

  const fetchCliente = async () => {
    const mockCliente: Cliente = {
      rg: "123456789",
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
        matricula: 1,
        ano: 2020,
        marca: "Toyota",
        modelo: "Corolla",
        placa: "ABC-1234",
        quantidade: 5,
      },
      {
        matricula: 2,
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
        automovel: {
          matricula: 1,
          ano: 2020,
          marca: "Toyota",
          modelo: "Corolla",
          placa: "ABC-1234",
          quantidade: 5,
        },
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
          cnpj: "12.345.678/0001-99",
          nome: "Banco XYZ",
          endereco: "Av. Norte, 789",
          quantidadeCarros: 15,
        },
        status: true,
        automovel: {
          matricula: 2,
          ano: 2019,
          marca: "Honda",
          modelo: "Civic",
          placa: "XYZ-5678",
          quantidade: 3,
        },
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
              {/* NOVO */}
              <button className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded">
                + Novo Pedido
              </button>
            </div>
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
                  {/* NOVO */}
                  <div className="mt-2 flex gap-2">
                    <button className="text-blue-600 underline">Editar</button>
                    <button className="text-red-500 underline">Cancelar</button>
                  </div>
                </li>
              ))}
            </ul>
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
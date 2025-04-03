"use client";

import React, { useState, useEffect } from "react";

// Tipos de dados
type Rendimento = {
  descricao: string;
  valor: number;
};

type Cliente = {
  nome: string;
  cpf: string;
  profissao: string;
  entidadeEmpregadora: string;
  rendimentos: Rendimento[];
};

type Automovel = {
  id: number;
  matricula: number;
  ano: number;
  marca: string;
  modelo: string;
  placa: string;
};

type Pedido = {
  id: number;
  status: string;
  duracao: number;
  tipoContrato: string;
  contratante: Cliente;
  automovel: Automovel;
};

export default function AgentePage() {
  const [selectedTab, setSelectedTab] = useState("pedidos");
  const [pedidos, setPedidos] = useState<Pedido[]>([]);
  const [automoveis, setAutomoveis] = useState<Automovel[]>([]);

  useEffect(() => {
    fetchPedidos();
    fetchAutomoveis();
  }, []);

  const fetchPedidos = () => {
    const mockPedidos: Pedido[] = [
        {
          id: 1,
          status: "pendente",
          duracao: 7,
          tipoContrato: "credito",
          contratante: {
            nome: "João da Silva",
            cpf: "111.222.333-44",
            profissao: "Engenheiro",
            entidadeEmpregadora: "Construtora ABC",
            rendimentos: [
              { descricao: "Salário", valor: 4000 },
              { descricao: "Freelance", valor: 1200 },
            ],
          },
          automovel: {
            id: 1,
            matricula: 101,
            ano: 2020,
            marca: "Toyota",
            modelo: "Corolla",
            placa: "XYZ-1234",
          },
        },
        {
          id: 2,
          status: "pendente",
          duracao: 10,
          tipoContrato: "debito",
          contratante: {
            nome: "João da Silva",
            cpf: "111.222.333-44",
            profissao: "Engenheiro",
            entidadeEmpregadora: "Construtora ABC",
            rendimentos: [
              { descricao: "Salário", valor: 4000 },
              { descricao: "Freelance", valor: 1200 },
            ],
          },
          automovel: {
            id: 2,
            matricula: 102,
            ano: 2019,
            marca: "Honda",
            modelo: "Civic",
            placa: "XYZ-5678",
          },
        },
      ];
    setPedidos(mockPedidos);
  };

  const fetchAutomoveis = () => {
    const mockAutomoveis: Automovel[] = [
        {
          id: 1,
          matricula: 101,
          ano: 2020,
          marca: "Toyota",
          modelo: "Corolla",
          placa: "XYZ-1234",
        },
        {
          id: 2,
          matricula: 102,
          ano: 2019,
          marca: "Honda",
          modelo: "Civic",
          placa: "XYZ-5678",
        },
      ];
    setAutomoveis(mockAutomoveis);
  };

  const handleAprovar = (id: number) => {
    alert(`Pedido #${id} aprovado!`);
    // Aqui irá a chamada real para API futuramente
  };

  const handleNegar = (id: number) => {
    alert(`Pedido #${id} negado.`);
    // Aqui irá a chamada real para API futuramente
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
                <li key={p.id} className="bg-gray-50 p-5 border rounded shadow-sm">
                  <div className="mb-2">
                    <p className="text-lg font-semibold">Pedido #{p.id}</p>
                    <p>Status: <strong>{p.status.toUpperCase()}</strong></p>
                    <p>Duração: {p.duracao} dias</p>
                    <p>Tipo de Contrato: {p.tipoContrato}</p>
                  </div>
                  <div className="mt-4">
                    <p className="font-medium text-gray-700">🚹 Cliente:</p>
                    <p>Nome: {p.contratante.nome}</p>
                    <p>CPF: {p.contratante.cpf}</p>
                    <p>Profissão: {p.contratante.profissao}</p>
                    <p>Entidade Empregadora: {p.contratante.entidadeEmpregadora}</p>
                    <p className="mt-1">Rendimentos:</p>
                    <ul className="ml-4 list-disc text-sm text-gray-600">
                      {p.contratante.rendimentos.map((r, idx) => (
                        <li key={idx}>{r.descricao}: R$ {r.valor.toFixed(2)}</li>
                      ))}
                    </ul>
                  </div>
                  <div className="mt-4">
                    <p className="font-medium text-gray-700">🚗 Automóvel:</p>
                    <p>{p.automovel.marca} {p.automovel.modelo} - Placa: {p.automovel.placa}</p>
                  </div>
                  <div className="mt-4 flex gap-4">
                    <button
                      className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
                      onClick={() => handleAprovar(p.id)}
                    >
                      ✅ Aprovar
                    </button>
                    <button
                      className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700"
                      onClick={() => handleNegar(p.id)}
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

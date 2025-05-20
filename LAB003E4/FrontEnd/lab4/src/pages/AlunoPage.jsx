"use client";

import React, { useEffect, useState } from "react";

export default function AlunoPage() {
  const [selectedTab, setSelectedTab] = useState("perfil");
  const [aluno, setAluno] = useState(null);
  const [extrato, setExtrato] = useState([]);
  const [vantagens, setVantagens] = useState([]);
  const [saldo, setSaldo] = useState(0);

  const usuarioId = typeof window !== "undefined" ? localStorage.getItem("usuarioId") : null;
  const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;

  useEffect(() => {
    if (usuarioId && token) {
      fetchAluno();
      fetchExtrato();
      fetchVantagens();
    }
  }, [usuarioId, token]);

  const fetchAluno = async () => {
    // Simulando resposta
    setAluno({
      nome: "João Silva",
      email: "joao@email.com",
      curso: "Engenharia de Software",
      instituicao: "PUC Minas"
    });
    setSaldo(420);
  };

  const fetchExtrato = async () => {
    setExtrato([
      { id: 1, tipo: "recebido", valor: 100, descricao: "Participação em aula", data: "2024-06-01" },
      { id: 2, tipo: "gasto", valor: 50, descricao: "Desconto no RU", data: "2024-06-10" },
    ]);
  };

  const fetchVantagens = async () => {
    setVantagens([
      { id: 1, nome: "Desconto RU", custo: 50, descricao: "Desconto no restaurante universitário", imagem: "https://via.placeholder.com/150" },
      { id: 2, nome: "Caneca Personalizada", custo: 100, descricao: "Caneca da instituição", imagem: "https://via.placeholder.com/150" },
    ]);
  };

  const handleTrocar = (vantagem) => {
    if (saldo < vantagem.custo) {
      alert("Saldo insuficiente.");
      return;
    }
    alert(`Resgatado com sucesso: ${vantagem.nome}. Código enviado por email.`);
    setSaldo((prev) => prev - vantagem.custo);
    setExtrato((prev) => [
      ...prev,
      { id: Date.now(), tipo: "gasto", valor: vantagem.custo, descricao: `Resgate: ${vantagem.nome}`, data: new Date().toISOString().split("T")[0] }
    ]);
  };

  return (
    
    <div className="flex min-h-screen bg-gray-100 text-black">
      <h1 className="text-2xl text-center mt-10">Página Aluno</h1>
      {/* Sidebar */}
      <aside className="w-72 bg-gray-800 shadow-lg min-h-screen p-6 flex flex-col text-white">
        <nav className="mt-6">
          {[
            { key: "perfil", label: "👤 Perfil" },
            { key: "extrato", label: "📄 Extrato" },
            { key: "vantagens", label: "🎁 Vantagens Disponíveis" },
          ].map((tab) => (
            <button
              key={tab.key}
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${
                selectedTab === tab.key
                  ? "bg-white text-black font-semibold"
                  : "hover:bg-gray-700"
              }`}
              onClick={() => setSelectedTab(tab.key)}
            >
              {tab.label}
            </button>
          ))}
        </nav>
      </aside>

      {/* Conteúdo principal */}
      <main className="flex-1 p-10">
        {selectedTab === "perfil" && aluno && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Perfil</h2>
            <p><strong>Nome:</strong> {aluno.nome}</p>
            <p><strong>Email:</strong> {aluno.email}</p>
            <p><strong>Curso:</strong> {aluno.curso}</p>
            <p><strong>Instituição:</strong> {aluno.instituicao}</p>
            <p className="mt-4 text-lg font-bold">Saldo de moedas: {saldo}</p>
          </div>
        )}

        {selectedTab === "extrato" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Extrato de Moedas</h2>
            <ul className="space-y-4">
              {extrato.map((item) => (
                <li key={item.id} className="border p-4 bg-gray-50 rounded">
                  <p><strong>Data:</strong> {item.data}</p>
                  <p><strong>Tipo:</strong> {item.tipo}</p>
                  <p><strong>Valor:</strong> {item.valor}</p>
                  <p><strong>Descrição:</strong> {item.descricao}</p>
                </li>
              ))}
            </ul>
          </div>
        )}

        {selectedTab === "vantagens" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-5xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Vantagens para Troca</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
              {vantagens.map((v) => (
                <div key={v.id} className="border p-4 bg-gray-50 rounded shadow">
                  <img src={v.imagem} alt={v.nome} className="w-full h-32 object-cover mb-2 rounded" />
                  <h3 className="text-lg font-bold">{v.nome}</h3>
                  <p className="text-sm text-gray-600">{v.descricao}</p>
                  <p className="font-semibold mt-2">Custo: {v.custo} moedas</p>
                  <button
                    className="mt-3 bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded"
                    onClick={() => handleTrocar(v)}
                  >
                    Trocar
                  </button>
                </div>
              ))}
            </div>
          </div>
        )}
      </main>
    </div>
  );
}

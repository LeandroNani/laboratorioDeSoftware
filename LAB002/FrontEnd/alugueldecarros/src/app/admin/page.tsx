"use client";

import React, { useState } from "react";

export default function AdminPage() {
  const [selectedTab, setSelectedTab] = useState("criarAgente");
  const [formData, setFormData] = useState({
    nome: "",
    email: "",
    senha: "",
    cnpj: "",
    endereco: "",
    quantidadeCarros: 0,
  });
  const [message, setMessage] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage("");

    const token = localStorage.getItem("token");

    try {
      const response = await fetch("http://localhost:5145/api/Admin/criar-agente", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          nome: formData.nome,
          email: formData.email,
          senha: formData.senha,
          cnpj: formData.cnpj,
          endereco: formData.endereco,
          quantidadeCarros: Number(formData.quantidadeCarros),
        }),
      });

      const result = await response.text();
      if (response.ok) {
        setMessage("✅ " + result);
        setFormData({
          nome: "",
          email: "",
          senha: "",
          cnpj: "",
          endereco: "",
          quantidadeCarros: 0,
        });
      } else {
        setMessage("❌ " + result);
      }
    } catch (err) {
      setMessage("Erro ao enviar requisição.");
      console.error(err);
    }
  };

  return (
    <div className="flex min-h-screen bg-gray-100 text-black">
      {/* Sidebar */}
      <aside className="w-72 bg-gray-800 shadow-lg min-h-screen p-6 flex flex-col text-white">
        <nav className="mt-6">
          <button
            className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg transition ${
              selectedTab === "criarAgente"
                ? "bg-white text-black font-semibold"
                : "hover:bg-gray-700"
            }`}
            onClick={() => setSelectedTab("criarAgente")}
          >
            🏢 Criar Agente
          </button>
          {/* Outras abas futuras */}
          <button
            className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${
              selectedTab === "pedidos"
                ? "bg-white text-black font-semibold"
                : "hover:bg-gray-700"
            }`}
            onClick={() => setSelectedTab("pedidos")}
          >
            📋 Todos os Pedidos
          </button>
        </nav>
      </aside>

      {/* Conteúdo principal */}
      <main className="flex-1 p-10">
        {selectedTab === "criarAgente" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-2xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Cadastro de Novo Agente</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
              <input
                type="text"
                name="nome"
                placeholder="Nome"
                value={formData.nome}
                onChange={handleChange}
                required
                className="w-full px-3 py-2 border rounded"
              />
              <input
                type="email"
                name="email"
                placeholder="Email"
                value={formData.email}
                onChange={handleChange}
                required
                className="w-full px-3 py-2 border rounded"
              />
              <input
                type="password"
                name="senha"
                placeholder="Senha"
                value={formData.senha}
                onChange={handleChange}
                required
                className="w-full px-3 py-2 border rounded"
              />
              <input
                type="text"
                name="cnpj"
                placeholder="CNPJ"
                value={formData.cnpj}
                onChange={handleChange}
                required
                className="w-full px-3 py-2 border rounded"
              />
              <input
                type="text"
                name="endereco"
                placeholder="Endereço"
                value={formData.endereco}
                onChange={handleChange}
                className="w-full px-3 py-2 border rounded"
              />
              <input
                type="number"
                name="quantidadeCarros"
                placeholder="Quantidade de Carros"
                value={formData.quantidadeCarros}
                onChange={handleChange}
                required
                className="w-full px-3 py-2 border rounded"
              />
              <button
                type="submit"
                className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
              >
                Criar Agente
              </button>
              {message && (
                <p
                  className={`text-sm mt-2 ${
                    message.startsWith("✅") ? "text-green-600" : "text-red-500"
                  }`}
                >
                  {message}
                </p>
              )}
            </form>
          </div>
        )}

        {selectedTab === "pedidos" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
            <h2 className="text-xl font-semibold">Listagem de Pedidos (em breve)</h2>
            <p className="text-gray-600 mt-2">Aqui será listado todos os pedidos feitos pelos clientes.</p>
          </div>
        )}
      </main>
    </div>
  );
}
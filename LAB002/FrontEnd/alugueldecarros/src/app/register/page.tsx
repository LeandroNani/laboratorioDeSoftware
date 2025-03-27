"use client";

import { useState } from 'react';

export default function Register() {
const [name, setName] = useState('');
const [email, setEmail] = useState('');
const [password, setPassword] = useState('');
const [role, setRole] = useState('cliente');

const handleRegister = async (e: React.FormEvent) => {
  e.preventDefault();

  // Aqui chamar o endpoint da API de registro quando estiver pronto
  console.log('Registro:', { name, email, password, role });
};

return (
  <div className="flex items-center justify-center min-h-screen bg-gray-100">
    <div className="bg-white p-6 rounded shadow-md w-full max-w-sm">
      <h1 className="text-xl text-gray-700 font-bold mb-4">Registro</h1>
      <form onSubmit={handleRegister}>
        <div className="mb-4">
          <label htmlFor="name" className="block text-sm font-medium text-gray-700">
            Nome
          </label>
          <input
            type="text"
            id="name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
            className="w-full px-3 py-2 border rounded"
          />
        </div>
        <div className="mb-4">
          <label htmlFor="email" className="block text-sm font-medium text-gray-700">
            Email
          </label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            className="w-full px-3 py-2 border rounded"
          />
        </div>
        <div className="mb-4">
          <label htmlFor="password" className="block text-sm font-medium text-gray-700">
            Senha
          </label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            className="w-full px-3 py-2 border rounded"
          />
        </div>
        <div className="mb-4">
          <label htmlFor="role" className="block text-sm font-medium text-gray-700">
            Função
          </label>
          <select
            id="role"
            value={role}
            onChange={(e) => setRole(e.target.value)}
            required
            className="w-full px-3 py-2 border rounded bg-white text-gray-700"
          >
            <option value="cliente">Cliente</option>
            <option value="agente_banco">Agente de Banco</option>
            <option value="agente_empresa">Agente da Empresa</option>
          </select>
        </div>
        <button
          type="submit"
          className="w-full bg-green-500 text-white py-2 rounded hover:bg-green-600"
        >
          Registrar
        </button>
      </form>
      <p className="mt-4 text-gray-700 text-sm text-center">
        Já tem uma conta? <a href="/login" className="text-blue-500 underline">Faça login</a>
      </p>
    </div>
  </div>
);
}
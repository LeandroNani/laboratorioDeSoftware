"use client";

import { useState } from 'react';

export default function Login() {
const [email, setEmail] = useState('');
const [password, setPassword] = useState('');

const handleLogin = async (e: React.FormEvent) => {
  e.preventDefault();

  // Aqui chamar o endpoint da API de login quando estiver pronto
  console.log('Login:', { email, password });
};

return (
  <div className="flex items-center justify-center min-h-screen bg-gray-100">
    <div className="bg-white p-6 rounded shadow-md w-full max-w-sm">
      <h1 className="text-xl font-bold mb-4">Login</h1>
      <form onSubmit={handleLogin}>
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
        <button
          type="submit"
          className="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600"
        >
          Entrar
        </button>
      </form>
      <p className="mt-4 text-sm text-center">
        Não tem uma conta? <a href="/register" className="text-blue-500 underline">Registre-se</a>
      </p>
    </div>
  </div>
);
}
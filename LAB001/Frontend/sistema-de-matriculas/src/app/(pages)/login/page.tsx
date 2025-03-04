'use client'

import React, { useState } from "react"
import { Eye, EyeOff } from "lucide-react"
import { Button } from "react-bootstrap"
import { Form } from "react-bootstrap"
import { login } from "@/api/login"
import Image from 'next/image'

export default function LoginPage() {
  const [showPassword, setShowPassword] = useState(false)
  const [formData, setFormData] = useState({
    numeroDePessoa: "",
    senha: "",
  })

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const response = await login({ ...formData, numeroDePessoa: Number(formData.numeroDePessoa) });
    console.log(response.data)
    // if (response.status === 200) window.location.href = "/"
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }))
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-red-50 to-red-100/50 flex items-center justify-center p-4">
      <div className="w-full max-w-4xl bg-white rounded-3xl shadow-xl overflow-hidden">
        <div className="grid lg:grid-cols-2">
          <div className="p-8 lg:p-12">
            <div className="space-y-3 mb-8">
              <h1 className="text-3xl font-bold text-gray-900">Login</h1>
              <p className="text-gray-600">Bem vindo(a) de volta</p>
            </div>

            <form onSubmit={handleSubmit} className="space-y-6">
              <div className="relative">
                <Form.Control
                  id="numeroDePessoa"
                  name="numeroDePessoa"
                  type="number"
                  placeholder=" "
                  value={formData.numeroDePessoa}
                  onChange={handleChange}
                  className="peer h-12 w-full border-b-2 border-gray-300 text-black placeholder-transparent focus:outline-none focus:border-red-500"
                  required
                />
                <label
                  htmlFor="numeroDePessoa"
                  className="absolute left-0 -top-3.5 text-gray-600 text-sm transition-all peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3 peer-focus:-top-3.5 peer-focus:text-red-500 peer-focus:text-sm"
                >
                  Número de pessoa
                </label>
              </div>

              <div className="relative">
                <Form.Control
                  id="senha"
                  name="senha"
                  type={showPassword ? "text" : "password"}
                  placeholder=" "
                  value={formData.senha}
                  onChange={handleChange}
                  className="peer h-12 w-full border-b-2 border-gray-300 text-black placeholder-transparent focus:outline-none focus:border-red-500"
                  required
                />
                <label
                  htmlFor="senha"
                  className="absolute left-0 -top-3.5 text-gray-600 text-sm transition-all peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3 peer-focus:-top-3.5 peer-focus:text-red-500 peer-focus:text-sm"
                >
                  Senha
                </label>
                <Button
                  onClick={() => setShowPassword(!showPassword)}
                  className="absolute right-3 top-3.5 text-gray-500 hover:text-gray-700"
                >
                  {showPassword ? <EyeOff className="h-5 w-5" /> : <Eye className="h-5 w-5" />}
                </Button>
              </div>


              <Button type="submit" className="w-full h-12 bg-red-500 hover:bg-red-600">
                Login
              </Button>
            </form>
          </div>
          <div className="hidden lg:block relative">
            <div className="absolute inset-0 bg-gradient-to-br from-red-100 to-red-50/90 z-0 flex items-center justify-center">
              <Image src="/brasao-escaladecinza.png" alt="Login" width={400} height={400} className="relative z-10" />
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

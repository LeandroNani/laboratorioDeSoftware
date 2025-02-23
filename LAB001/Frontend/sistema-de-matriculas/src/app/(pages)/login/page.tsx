"use client"

import type React from "react"

import { useState } from "react"
import { Eye, EyeOff } from "lucide-react"
import { Button } from "react-bootstrap"
import { Form } from "react-bootstrap"

export default function LoginPage() {
  const [showPassword, setShowPassword] = useState(false)
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  })

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    console.log(formData)
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
            {/* Header Section */}
            <div className="space-y-3 mb-8">
              <h1 className="text-3xl font-bold text-gray-900">Login</h1>
              <p className="text-gray-600">Bem vindo(a) de volta</p>
            </div>

            {/* Form Section */}
            <form onSubmit={handleSubmit} className="space-y-6">
                <label htmlFor="email" className="text-black">Email</label>
                <div className="relative">
                  <Form.Control
                    id="email"
                    name="email"
                    type={showPassword ? "text" : "email"}
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    className="h-12 pr-10 text-black"
                    required
                  />
              </div>

              <div className="space-y-2">
                <label htmlFor="password" className="text-black">Senha</label>
                <div className="relative">
                  <Form.Control
                    id="password"
                    name="password"
                    type={showPassword ? "text" : "password"}
                    placeholder="Senha"
                    value={formData.password}
                    onChange={handleChange}
                    className="h-12 pr-10 text-black"
                    required
                  />
                  <button
                    type="button"
                    onClick={() => setShowPassword(!showPassword)}
                    className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-700"
                  >
                    {showPassword ? <EyeOff className="h-5 w-5" /> : <Eye className="h-5 w-5" />}
                  </button>
                </div>
              </div>

              <div className="flex items-center justify-between">
                <label className="flex items-center gap-2">
                  <input type="checkbox" className="rounded border-gray-300 text-red-500 focus:ring-red-500" />
                  <span className="text-sm text-gray-600">Remember me</span>
                </label>
                <a href="#" className="text-sm font-medium text-red-600 hover:text-red-500">
                  Esqueceu a senha
                </a>
              </div>

              <Button type="submit" className="w-full h-12 bg-red-500 hover:bg-red-600">
                Login
              </Button>

              <p className="text-center text-sm text-gray-600">
                Não tem uma conta?{" "}
                <a href="#" className="font-medium text-red-600 hover:text-red-500">
                  Registre-se
                </a>
              </p>
            </form>
          </div>

          {/* Image Section */}
          <div className="hidden lg:block relative">
            <div className="absolute inset-0 bg-gradient-to-br from-red-100 to-red-50/90" />
            <div className="absolute inset-0 flex items-center justify-center p-8">
              {/* <Image
                src="https://hebbkx1anhila5yf.public.blob.vercel-storage.com/robot_img-6laTjTJeW0iHaHwxvzCsi6UrMVm1t4.png"
                alt="Robot illustration"
                width={300}
                height={300}
                className="w-full h-auto max-w-md transform -rotate-6"
              /> */}
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}


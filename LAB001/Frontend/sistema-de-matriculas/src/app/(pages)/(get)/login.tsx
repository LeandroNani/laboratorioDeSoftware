'use client'

import React, { useState } from "react"
import { Eye, EyeOff } from "lucide-react"
import { Button } from "react-bootstrap"
import { Form } from "react-bootstrap"
import { login } from "@/api/login"
import Image from "next/image"
import { toast } from "react-toastify"
import "react-toastify/dist/ReactToastify.css"
import { AxiosError } from "axios"
import { ToastContainer } from "react-toastify"
import { Pessoa } from "@/@types/pessoa.type"
import { createAluno } from "@/api/admin"
import { Curso } from "@/@types/curso.type"

interface PageProps {
  cursos: Curso[];
}

export const Page: React.FC<PageProps> = ({ cursos }) => {
  const [showPassword, setShowPassword] = useState(false)
  const [isSignUp, setIsSignUp] = useState(false)
  const [formData, setFormData] = useState<{
    numeroDePessoa: string;
    nome: string;
    senha: string;
    email: string;
    curso: Curso | undefined;
    matricula: null;
  }>({
    numeroDePessoa: "",
    nome: "",
    senha: "",
    email: "",
    curso: undefined,
    matricula: null
  })

  // Retrieve the courses (adjust if asynchronous)
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const curso = formData.curso
    const numeroDeMatricula = Math.floor(100000 + Math.random() * 900000).toString()
    const numeroDePessoa = Math.floor(100000 + Math.random() * 900000).toString();
    if (isSignUp) {
      try {
        await createAluno({
          numeroDePessoa: numeroDePessoa,
          email: formData.email,
          nome: formData.nome,
          senha: formData.senha,
          matricula: {
            numeroDeMatricula: numeroDeMatricula,
            ativa: true,
            planoDeEnsino: curso?.disciplinas || [],
            mensalidade: 0,
          },
          matriculaId: numeroDeMatricula,
          curso: curso,
        })
        toast.success("Perfil cadastrado, seu número de pessoa foi enviado via email.", {
          position: "top-right",
          autoClose: 3000,
          theme: "colored",
          onClose: () => {
            window.location.href = `/profile/${numeroDePessoa}`
          }
        })
      } catch (error) {
        toast.error(`Erro ao criar conta. Tente novamente. ${error}`, {
          position: "top-right",
          autoClose: 3000,
          theme: "colored",
        })
      }
      return
    }

    // Login flow
    try {
      const response = await login({
        ...formData,
        numeroDePessoa: String(formData.numeroDePessoa),
      })
      const pessoa: Partial<Pessoa> = response.data

      if (response.status === 200 && pessoa.type === "STUDENT") {
        window.location.href = `/profile/${response.data.numeroDePessoa}`
      } else if (pessoa.type === "ADMIN") {
        window.location.href = "/admin"
      } else {
        window.location.href = `/professor/${response.data.numeroDePessoa}`
      }
    } catch (error) {
      if (error instanceof AxiosError && error.response && error.status === 401) {
        toast.error(error.response.data.error, {
          position: "top-right",
          autoClose: 3000,
          theme: "colored",
        })
        return
      }
      if (error instanceof AxiosError && error.response && error.status === 404) {
        toast.error(error.response.data.error, {
          position: "top-right",
          autoClose: 3000,
          theme: "colored",
        })
        return
      }
      toast.error("Erro ao tentar fazer login. Tente novamente.", {
        position: "top-right",
        autoClose: 3000,
        theme: "colored",
      })
    }
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target

    // For the course field, find and store the whole object
    if (name === "curso") {
      const selectedCurso = cursos.find((curso) => curso.id === value) || undefined
      setFormData((prev) => ({
        ...prev,
        curso: selectedCurso,
      }))
    } else {
      setFormData((prev) => ({
        ...prev,
        [name]: value,
      }))
    }
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-red-50 to-red-100/50 flex items-center justify-center p-4">
      <ToastContainer />
      <div className="w-full max-w-4xl bg-white rounded-3xl shadow-xl overflow-hidden">
        <div className="grid lg:grid-cols-2">
          <div className="p-8 lg:p-12">
            <div className="space-y-3 mb-8">
              <h1 className="text-3xl font-bold text-gray-900">
                {isSignUp ? "Cadastro" : "Login"}
              </h1>
              <p className="text-gray-600">
                {isSignUp ? "Crie sua conta" : "Bem vindo(a) de volta"}
              </p>
            </div>

            <form onSubmit={handleSubmit} className="space-y-6">
              {!isSignUp && (
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
              )}

              {isSignUp && (
                <>
                  <div className="relative">
                    <Form.Control
                      id="email"
                      name="email"
                      type="email"
                      placeholder=" "
                      value={formData.email}
                      onChange={handleChange}
                      className="peer h-12 w-full border-b-2 border-gray-300 text-black placeholder-transparent focus:outline-none focus:border-red-500"
                      required
                    />
                    <label
                      htmlFor="email"
                      className="absolute left-0 -top-3.5 text-gray-600 text-sm transition-all peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3 peer-focus:-top-3.5 peer-focus:text-red-500 peer-focus:text-sm"
                    >
                      Email
                    </label>
                  </div>

                  <div className="relative">
                    <Form.Control
                      id="nome"
                      name="nome"
                      type="text"
                      placeholder=" "
                      value={formData.nome}
                      onChange={handleChange}
                      className="peer h-12 w-full border-b-2 border-gray-300 text-black placeholder-transparent focus:outline-none focus:border-red-500"
                      required
                    />
                    <label
                      htmlFor="nome"
                      className="absolute left-0 -top-3.5 text-gray-600 text-sm transition-all peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3 peer-focus:-top-3.5 peer-focus:text-red-500 peer-focus:text-sm"
                    >
                      Nome
                    </label>
                  </div>

                  <div className="relative">
                    <Form.Control
                      as="select"
                      id="curso"
                      name="curso"
                      value={formData.curso?.id || ""}
                      onChange={handleChange}
                      className="peer h-12 w-full border-b-2 border-gray-300 text-black focus:outline-none focus:border-red-500"
                      required
                    >
                      <option value="" disabled>
                        Selecione seu curso
                      </option>
                      {cursos.map((curso: Curso) => (
                        <option key={curso.id} value={curso.id}>
                          {curso.nome}
                        </option>
                      ))}
                    </Form.Control>
                    <label
                      htmlFor="curso"
                      className="absolute left-0 -top-3.5 text-gray-600 text-sm transition-all peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 peer-placeholder-shown:top-3 peer-focus:-top-3.5 peer-focus:text-red-500 peer-focus:text-sm"
                    >
                      Curso
                    </label>
                  </div>
                </>
              )}

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
                  type="button"
                >
                  {showPassword ? (
                    <EyeOff className="h-5 w-5" />
                  ) : (
                    <Eye className="h-5 w-5" />
                  )}
                </Button>
              </div>

              <Button type="submit" className="w-full h-12 bg-red-500 hover:bg-red-600">
                {isSignUp ? "Cadastrar" : "Login"}
              </Button>
            </form>

            <div className="text-center mt-4">
              {isSignUp ? (
                <span>
                  Já tem uma conta?{" "}
                  <button
                    type="button"
                    className="text-red-500"
                    onClick={() => setIsSignUp(false)}
                  >
                    Fazer login
                  </button>
                </span>
              ) : (
                <span>
                  Ainda não tem uma conta?{" "}
                  <button
                    type="button"
                    className="text-red-500"
                    onClick={() => setIsSignUp(true)}
                  >
                    Criar conta
                  </button>
                </span>
              )}
            </div>
          </div>
          <div className="hidden lg:block relative">
            <div className="absolute inset-0 bg-gradient-to-br from-red-100 to-red-50/90 z-0 flex items-center justify-center">
              <Image
                src="/brasao-escaladecinza.png"
                alt="Login"
                width={300}
                height={300}
                className="relative z-10"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

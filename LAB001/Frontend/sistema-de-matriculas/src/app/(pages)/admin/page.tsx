'use client'

import React, { useState } from 'react';
import { Aluno, Matricula } from '@/types/aluno.type';
import { Disciplina } from '@/types/disciplina.type';
import { Curso } from '@/types/curso.type';
import { Professor } from '@/types/professor.type';
import { toast, ToastContainer } from 'react-toastify';
import Navbar from '@/components/Navbar';

type Curriculo = {
    cursos: Curso[];
    disciplinas: Disciplina[];
    professores: Professor[];
    alunos: Aluno[];
};

const AdminPage: React.FC = () => {
    // Data arrays
    const [cursos, setCursos] = useState<Curso[]>([]);
    const [professores, setProfessores] = useState<Professor[]>([]);
    const [disciplinas, setDisciplinas] = useState<Disciplina[]>([]);
    const [alunos, setAlunos] = useState<Aluno[]>([]);
    const [curriculo, setCurriculo] = useState<Curriculo | null>(null);

    // Form states for Curso
    const [cursoNome, setCursoNome] = useState('');
    const [cursoCreditos, setCursoCreditos] = useState('');

    // Form states for Professor
    const [professorNome, setProfessorNome] = useState('');
    const [professorSenha, setProfessorSenha] = useState('');
    const [professorNivel, setProfessorNivel] = useState('');

    // Form states for Disciplina
    const [disciplinaNome, setDisciplinaNome] = useState('');
    const [disciplinaPreco, setDisciplinaPreco] = useState('');
    const [disciplinaPeriodo, setDisciplinaPeriodo] = useState('');
    const [disciplinaCampus, setDisciplinaCampus] = useState('');
    // Using professor's numeroDePessoa as identifier for selection
    const [selectedProfessorNumero, setSelectedProfessorNumero] = useState<number | null>(null);

    // Form states for Aluno
    const [alunoNome, setAlunoNome] = useState('');
    const [alunoEmail, setAlunoEmail] = useState('');
    const [alunoSenha, setAlunoSenha] = useState('');
    const [matriculaNumero, setMatriculaNumero] = useState('');
    const [matriculaMensalidade, setMatriculaMensalidade] = useState('');
    const [selectedCursoIdForAluno, setSelectedCursoIdForAluno] = useState<string>('');

    // Handler to add a Curso
    const addCurso = () => {
        if (!cursoNome.trim() || !cursoCreditos.trim()) {
            toast.error("Informe o nome do curso e os créditos", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
            })
            return;
        }
        const novoCurso: Curso = {
            id: Date.now().toString(),
            nome: cursoNome,
            numeroDeCreditos: parseInt(cursoCreditos, 10),
            alunos: [],
            disciplinas: [],
        };
        setCursos([...cursos, novoCurso]);
        setCursoNome('');
        setCursoCreditos('');
        toast.success("Novo curso adicionado", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        })
    };

    // Handler to add a Professor
    const addProfessor = () => {
        if (!professorNome.trim() || !professorSenha.trim() || !professorNivel.trim()) {
            toast.error("Informe todos os campos necessários para adicionar um professor", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
            })
            return;
        }
        const novoProfessor: Professor = {
            numeroDePessoa: Date.now(),
            nome: professorNome,
            senha: professorSenha,
            disciplinas: [],
            nivelEscolar: professorNivel,
        };
        setProfessores([...professores, novoProfessor]);
        setProfessorNome('');
        setProfessorSenha('');
        setProfessorNivel('');
        toast.success("Novo professor adicionado", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        })
    };

    // Handler to add a Disciplina
    const addDisciplina = () => {
        if (!disciplinaNome.trim() || !disciplinaPreco.trim() || !disciplinaPeriodo.trim() || !disciplinaCampus.trim()) {
            toast.error("Informe todos os campos necessários para adicionar uma disciplina", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
            })
            return;
        }
        // Find the selected professor, or use a default placeholder if none is selected.
        const professorSelecionado =
            professores.find(p => p.numeroDePessoa === selectedProfessorNumero) || {
                numeroDePessoa: 0,
                nome: 'N/A',
                senha: '',
                disciplinas: [],
                nivelEscolar: '',
            };

        const novaDisciplina: Disciplina = {
            id: Date.now().toString(),
            nome: disciplinaNome,
            isActive: true,
            alunos: [],
            preco: parseFloat(disciplinaPreco),
            periodo: disciplinaPeriodo,
            campus: disciplinaCampus,
            disciplinasNecessarias: [],
            professor: professorSelecionado,
        };
        setDisciplinas([...disciplinas, novaDisciplina]);
        setDisciplinaNome('');
        setDisciplinaPreco('');
        setDisciplinaPeriodo('');
        setDisciplinaCampus('');
        setSelectedProfessorNumero(null);
        toast.success("Nova disciplina adicionada", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        })
    };

    // Handler to add an Aluno
    const addAluno = () => {
        if (!alunoNome.trim() || !alunoEmail.trim() || !alunoSenha.trim() || !matriculaNumero.trim() || !matriculaMensalidade.trim()) {
            toast.error("Informe todos os campos necessários para adicionar um aluno", {
                position: "top-right",
                autoClose: 3000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "colored",
            })
            return;
        }
        const cursoSelecionado = cursos.find(c => c.id === selectedCursoIdForAluno);
        const novaMatricula: Matricula = {
            numeroDeMatricula: parseInt(matriculaNumero, 10),
            ativa: true,
            planoDeEnsino: [],
            mensalidade: parseFloat(matriculaMensalidade),
        };
        const novoAluno: Aluno = {
            numeroDePessoa: Date.now(),
            nome: alunoNome,
            senha: alunoSenha,
            email: alunoEmail,
            matricula: novaMatricula,
            curso: cursoSelecionado,
            disciplinasCursadas: [],
        };
        setAlunos([...alunos, novoAluno]);
        setAlunoNome('');
        setAlunoEmail('');
        setAlunoSenha('');
        setMatriculaNumero('');
        setMatriculaMensalidade('');
        setSelectedCursoIdForAluno('');
        toast.success("Novo aluno adicionado", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        })
    };

    // Handler to aggregate all data into a Curriculo
    const saveCurriculo = () => {
        const novoCurriculo: Curriculo = {
            cursos,
            disciplinas,
            professores,
            alunos,
        };
        setCurriculo(novoCurriculo);
        toast.success("Curriculo salvo", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "colored",
        })
        console.log('Curriculo:', novoCurriculo);
    };

    return (
        <>
            <Navbar />
            <div className="min-h-screen p-8 bg-zinc-800">
                <ToastContainer />
                <h1 className="text-3xl font-bold mb-6">Admin Dashboard</h1>
                <div className="grid grid-cols-1 gap-6 bg-zinc-700 text-black">
                    {/* Curso Registration */}
                    <div className="bg-white shadow-md rounded p-6">
                        <h2 className="text-xl font-semibold mb-4">Registrar Curso</h2>
                        <div className="flex space-x-2 mb-4">
                            <input
                                type="text"
                                placeholder="Curso Nome"
                                value={cursoNome}
                                onChange={(e) => setCursoNome(e.target.value)}
                                className="border rounded p-2 flex-1"
                            />
                            <input
                                type="number"
                                placeholder="Numero de Creditos"
                                value={cursoCreditos}
                                onChange={(e) => setCursoCreditos(e.target.value)}
                                className="border rounded p-2 w-40"
                            />
                        </div>
                        <button onClick={addCurso} className="bg-blue-500 text-white px-4 py-2 rounded">
                            Add Curso
                        </button>
                        {cursos.length > 0 && (
                            <ul className="mt-4">
                                {cursos.map((curso) => (
                                    <li key={curso.id} className="text-gray-700">
                                        {curso.nome} - {curso.numeroDeCreditos} Créditos
                                    </li>
                                ))}
                            </ul>
                        )}
                    </div>

                    {/* Professor Registration */}
                    <div className="bg-white shadow-md rounded p-6">
                        <h2 className="text-xl font-semibold mb-4">Registrar Professor</h2>
                        <div className="mb-4">
                            <input
                                type="text"
                                placeholder="Professor Nome"
                                value={professorNome}
                                onChange={(e) => setProfessorNome(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <input
                                type="password"
                                placeholder="Senha"
                                value={professorSenha}
                                onChange={(e) => setProfessorSenha(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <input
                                type="text"
                                placeholder="Nivel Escolar"
                                value={professorNivel}
                                onChange={(e) => setProfessorNivel(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <button onClick={addProfessor} className="bg-green-500 text-white px-4 py-2 rounded">
                            Add Professor
                        </button>
                        {professores.length > 0 && (
                            <ul className="mt-4">
                                {professores.map((prof) => (
                                    <li key={prof.numeroDePessoa} className="text-gray-700">
                                        {prof.nome} - {prof.nivelEscolar}
                                    </li>
                                ))}
                            </ul>
                        )}
                    </div>

                    {/* Disciplina Registration */}
                    <div className="bg-white shadow-md rounded p-6">
                        <h2 className="text-xl font-semibold mb-4">Registrar Disciplina</h2>
                        <div className="mb-4">
                            <input
                                type="text"
                                placeholder="Disciplina Nome"
                                value={disciplinaNome}
                                onChange={(e) => setDisciplinaNome(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <input
                                type="number"
                                placeholder="Preco"
                                value={disciplinaPreco}
                                onChange={(e) => setDisciplinaPreco(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <input
                                type="text"
                                placeholder="Periodo"
                                value={disciplinaPeriodo}
                                onChange={(e) => setDisciplinaPeriodo(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <input
                                type="text"
                                placeholder="Campus"
                                value={disciplinaCampus}
                                onChange={(e) => setDisciplinaCampus(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <select
                                value={selectedProfessorNumero || ''}
                                onChange={(e) => setSelectedProfessorNumero(Number(e.target.value))}
                                className="border rounded p-2 w-full"
                            >
                                <option value="">Select Professor (optional)</option>
                                {professores.map((prof) => (
                                    <option key={prof.numeroDePessoa} value={prof.numeroDePessoa}>
                                        {prof.nome}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <button onClick={addDisciplina} className="bg-purple-500 text-white px-4 py-2 rounded">
                            Add Disciplina
                        </button>
                        {disciplinas.length > 0 && (
                            <ul className="mt-4">
                                {disciplinas.map((disc) => (
                                    <li key={disc.id} className="text-gray-700">
                                        {disc.nome} - {disc.campus}
                                    </li>
                                ))}
                            </ul>
                        )}
                    </div>

                    {/* Aluno Registration */}
                    <div className="bg-white shadow-md rounded p-6">
                        <h2 className="text-xl font-semibold mb-4">Registrar Aluno</h2>
                        <div className="mb-4">
                            <input
                                type="text"
                                placeholder="Aluno Nome"
                                value={alunoNome}
                                onChange={(e) => setAlunoNome(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <input
                                type="email"
                                placeholder="Aluno Email"
                                value={alunoEmail}
                                onChange={(e) => setAlunoEmail(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="mb-4">
                            <input
                                type="password"
                                placeholder="Senha"
                                value={alunoSenha}
                                onChange={(e) => setAlunoSenha(e.target.value)}
                                className="border rounded p-2 w-full"
                            />
                        </div>
                        <div className="flex space-x-2 mb-4">
                            <input
                                type="number"
                                placeholder="Numero de Matricula"
                                value={matriculaNumero}
                                onChange={(e) => setMatriculaNumero(e.target.value)}
                                className="border rounded p-2 flex-1"
                            />
                            <input
                                type="number"
                                placeholder="Mensalidade"
                                value={matriculaMensalidade}
                                onChange={(e) => setMatriculaMensalidade(e.target.value)}
                                className="border rounded p-2 flex-1"
                            />
                        </div>
                        <div className="mb-4">
                            <select
                                value={selectedCursoIdForAluno}
                                onChange={(e) => setSelectedCursoIdForAluno(e.target.value)}
                                className="border rounded p-2 w-full"
                            >
                                <option value="">Selecionar Curso (opcional)</option>
                                {cursos.map((curso) => (
                                    <option key={curso.id} value={curso.id}>
                                        {curso.nome}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <button onClick={addAluno} className="bg-red-500 text-white px-4 py-2 rounded">
                            Add Aluno
                        </button>
                        {alunos.length > 0 && (
                            <ul className="mt-4">
                                {alunos.map((aluno) => (
                                    <li key={aluno.numeroDePessoa} className="text-gray-700">
                                        {aluno.nome}
                                    </li>
                                ))}
                            </ul>
                        )}
                    </div>

                    {/* Save Curriculo */}
                    <div className="bg-white shadow-md rounded p-6">
                        <button onClick={saveCurriculo} className="w-full bg-indigo-600 text-white px-4 py-2 rounded">
                            Save Curriculo
                        </button>
                        {curriculo && (
                            <div className="mt-4">
                                <h3 className="text-lg font-semibold">Dados do curriculo:</h3>
                                <pre className="bg-gray-200 p-2 rounded text-sm overflow-x-auto">
                                    {JSON.stringify(curriculo, null, 2)}
                                </pre>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </>
    );
};

export default AdminPage;

import { Curso } from "@/@types/curso.type";
import { Disciplina } from "@/@types/disciplina.type";
import { Professor } from "@/@types/professor.type";
import { Aluno } from "@/@types/aluno.type";
import { Curriculo } from "@/@types/curriculo.type";
import api from "./lib/api";
export async function createCurso(curso: Curso) {
    const response = await api.post("/curso/novo-curso", curso)
    return response
}

export async function createDisciplina(disciplina: Disciplina) {
    const response = await api.post("/disciplina/nova-disciplina", disciplina);
    return response;
}

export async function createProfessor(professor: Partial<Professor>) {
    const response = await api.post("/professor/novo-professor", professor);
    return response;
}

export async function createAluno(aluno: Partial<Aluno>) {
    const response = await api.post("/aluno/novo-aluno", aluno);
    return response;
}

export async function efetuarMatricula(aluno: Aluno){
    const response = await api.put("/aluno/efetuar-matricula", aluno);
    return response;
}

export async function createCurriculo(curriculo: Curriculo) {
    const response = await api.post("/curriculo/novo-curriculo", curriculo);
    return response.data as Curriculo;
}

export async function updateCurriculo(curriculo: Curriculo){
    const response = await api.put("/curriculo/atualizar-curriculo", curriculo);
    return response.data as Curriculo;
}

export async function getCurriculo() {
    const response = await api.get("/curriculo/get-curriculo")
    return response.data as Curriculo
}

export async function getDisciplinas() {
    const response = await api.get("/disciplina/listar");
    return response.data as Disciplina[];
}

export async function getCursos() {
    const response = await api.get("/curso/listar");
    return response.data as Curso[]
}

export async function getProfessores() {
    const response = await api.get("/professor/listar");
    return response.data as Professor[];
}

export async function getAlunos() {
    const response = await api.get("/aluno/listar");
    return response.data as Aluno[]
}

export async function updateAluno(aluno: Aluno) {
    const response = await api.put("/aluno/atualizar-aluno", aluno);
    return response.data as Aluno;
}

export async function updateCurso(curso: Curso) {
    const response = await api.put("/curso/atualizar-curso", curso);
    return response.data as Curso;
}

export async function updateDisciplina(disciplina: Disciplina) {
    const response = await api.put("/disciplina/atualizar-disciplina", disciplina);
    return response.data as Disciplina;
}
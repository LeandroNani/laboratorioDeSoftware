import { Aluno } from "./aluno.type";
import { Disciplina } from "./disciplina.type";

export type Curso = {
    nome: string;
    numeroDeCreditos: number;
    alunos: Aluno[];
    disciplinas: Disciplina[];
}
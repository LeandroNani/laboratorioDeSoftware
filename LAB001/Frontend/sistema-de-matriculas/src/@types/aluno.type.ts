import { Curso } from "./curso.type";
import { Disciplina } from "./disciplina.type";
import { Pessoa } from "./pessoa.type";

export type Aluno = Pessoa & {
    email?: string;
    curso: Curso
    matricula: Matricula;
    disciplinasCursadas: Disciplina[];
}

export type Matricula = {
    ativa: boolean,
    planoDeEnsino: Disciplina[],
    mensalidade: number
}
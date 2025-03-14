import { Curso } from "./curso.type";
import { Disciplina } from "./disciplina.type";
import { Pessoa } from "./pessoa.type";

export type Aluno = Pessoa & {
    email?: string;
    curso: Curso
    matricula: Matricula;
    type?: string
    disciplinasCursadas: Disciplina[];
    matriculaId?: string
}

export type Matricula = {
    ativa: boolean,
    planoDeEnsino: Disciplina[],
    mensalidade: number
    numeroDeMatricula?: string
    paga: boolean
}

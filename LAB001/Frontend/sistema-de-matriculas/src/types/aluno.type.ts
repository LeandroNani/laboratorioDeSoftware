import { Curso } from "./curso.type";
import { Disciplina } from "./disciplina.type";
import { Pessoa } from "./pessoa.type";

export type Aluno = Pessoa & {
    email?: string;
    matricula: Matricula;
    curso?: Curso;
    disciplinasCursadas: Disciplina[];
}

export type Matricula = {
    numeroDeMatricula: number,
    ativa: boolean,
    planoDeEnsino: Disciplina[],
    mensalidade: number
}
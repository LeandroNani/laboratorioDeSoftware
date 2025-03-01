import { Curso } from "./curso.type";
import { Disciplina } from "./disciplina.type";
import { Pessoa } from "./pessoa.type";

export type Aluno = Pessoa & {
    matricula: string;
    mensalidade: number;
    email?: string;
    curso?: Curso;
    planoDeEnsino: Disciplina[];
    disciplinasCursadas: Disciplina[];
}
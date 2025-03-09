import { Disciplina } from "./disciplina.type";
import { Pessoa } from "./pessoa.type";

export type Professor = Pessoa & {
    disciplinas: Disciplina[];
    nivelEscolar: string;
    email: string
}
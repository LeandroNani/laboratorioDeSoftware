import { LoginRequest } from "@/@types/login.type";
import api from "./lib/api";

export async function login({numeroDePessoa, senha}: LoginRequest ) {
    return api.post('/auth/login', { numeroDePessoa, senha });
};

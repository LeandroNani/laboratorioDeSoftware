import { LoginRequest } from "@/types/login.type";
import apiService from "./lib/api";

export async function login({numeroDePessoa, senha}: LoginRequest ) {
    return apiService.post('/auth/login', { numeroDePessoa, senha });
};
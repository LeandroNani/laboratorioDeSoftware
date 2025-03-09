'use server'
import { Page } from '../login'
import { getCursos } from '@/api/admin'

export default async function RedirectToLoginpage() {
    const cursos = await getCursos()
    const cursosArray = Object.values(cursos);
    return <Page cursos={cursosArray} />
}


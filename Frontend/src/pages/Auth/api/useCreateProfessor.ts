import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";

export const useCreateProfessor = () => {
    const { users } = urls;
    const { request, loading } = useHttp();

    const createProfessor = async (data: any) => {
        const res = await request(users + 'professor', HTTP_METHOD.POST, data);
        return res?.success;
    };

    return {
        createProfessor: createProfessor,
        createUserLoading: loading
    };
};

import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";

export const useCreateStudent = () => {
    const { users } = urls;
    const { request, loading } = useHttp();

    const createStudent = async (data: any) => {
        const res = await request(users + "/student", HTTP_METHOD.POST, data);

        return res?.success;
    };

    return {
        createStudent,
        createUserLoading: loading
    };
};

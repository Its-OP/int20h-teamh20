import { useHttp } from "../../../shared/api/useHttp.ts";

export const useCreateStudent = () => {
    const { request, loading } = useHttp();

    const createStudent = async (data: any) => {};

    return {
        createStudent,
        createUserLoading: loading
    };
};

import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";

export const useCreateGroup = () => {
    const { groups } = urls;
    const { request, loading } = useHttp();

    const createGroup = async (data: any) => {
        const res = await request(groups, HTTP_METHOD.POST, data);

        return res?.result;
    };

    return {
        createGroup,
        createGroupLoading: loading
    };
};

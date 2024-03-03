import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";

export const useCreateNotification = () => {
    const { messages } = urls;
    const { request, loading } = useHttp();

    const createNotification = async (data: any) => {
        const res = await request(messages, HTTP_METHOD.POST, data);

        return res?.success;
    };

    return { createNotification, createNotyLoading: loading };
};

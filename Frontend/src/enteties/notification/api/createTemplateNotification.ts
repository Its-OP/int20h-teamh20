import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";

export const useCreateTemplateNotification = () => {
    const { templatesMessages } = urls;
    const { request, loading } = useHttp();

    const createTemplateNotification = async (data: any) => {
        const res = await request(templatesMessages, HTTP_METHOD.POST, data);

        return res?.success;
    };

    return { createTemplateNotification, createNotyLoading: loading };
};

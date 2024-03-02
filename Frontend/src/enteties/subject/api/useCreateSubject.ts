import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { Subject } from "../../../shared/model/types.ts";
import { urls } from "../../../shared/api/api.ts";

const useCreateSubject = () => {
    const { subject } = urls;
    const { request, loading } = useHttp();

    const createSubject = async (data: Subject) => {
        const res = await request(subject, HTTP_METHOD.POST, data);

        return res?.result;
    };

    return {
        subjectCreateLoading: loading,
        createSubject
    };
};
export default useCreateSubject;

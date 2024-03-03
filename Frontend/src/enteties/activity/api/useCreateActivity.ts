import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";

export const useCreateActivity = () => {
    const { activity } = urls;
    const { request, loading } = useHttp();

    const createActivity = async (data: {
        subjectId: number;
        activityTypeId: number;
        maxScore: number;
        dateTime: string;
        body: { isAbsent: boolean; score: number; studentId: number }[];
    }) => {
        const res = await request(
            `${activity}subject/${data.subjectId}/at/${data.dateTime}/type/${data.activityTypeId}/maxScore/${data.maxScore}`,
            HTTP_METHOD.POST,
            data.body
        );

        return res?.success;
    };

    return { createActivity, createActivityLoading: loading };
};

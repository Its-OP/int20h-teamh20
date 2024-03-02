import { useState } from "react";
import { StudentComplete } from "../../../shared/model/types.ts";
import { useHttp } from "../../../shared/api/useHttp.ts";
import { studentCompleteMock } from "../../../shared/model/moc.ts";
import { urls } from "../../../shared/api/api.ts";

export const useStudent = () => {
    const { students } = urls;
    const { request, loading } = useHttp();

    const [student, setStudent] =
        useState<StudentComplete>(studentCompleteMock);

    const fetchStudent = async (id: string) => {
        const res = await request(students + id);

        setStudent(res);
    };
    return { studentLoading: loading, student, fetchStudent };
};

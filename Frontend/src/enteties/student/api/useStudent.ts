import { useState } from "react";
import { StudentComplete } from "../../../shared/model/types.ts";
import { useHttp } from "../../../shared/api/useHttp.ts";
import { studentCompleteMock } from "../../../shared/model/moc.ts";

export const useStudent = () => {
    const { request, loading } = useHttp();

    const [student, setStudent] =
        useState<StudentComplete>(studentCompleteMock);

    const fetchStudent = async (id: number) => {
        const res = await request(``);

        setStudent(res);
    };
    return { studentLoading: loading, student, fetchStudent };
};

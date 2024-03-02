import { useHttp } from "../../../shared/api/useHttp.ts";
import { useState } from "react";
import { Group } from "../../../shared/model/types.ts";

export const useGroups = () => {
    const { request, loading } = useHttp();
    const [groups, setGroups] = useState<Group[]>([]);

    const fetchGroups = async () => {
        const res = await request(``);

        setGroups(res);
    };

    return {
        groupsLoading: loading,
        groups,
        fetchGroups
    };
};

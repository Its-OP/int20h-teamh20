import { useHttp } from "../../../shared/api/useHttp.ts";
import { useState } from "react";
import { Group } from "../../../shared/model/types.ts";
import { urls } from "../../../shared/api/api.ts";

export const useGroups = () => {
    const { groups: groupsUrl } = urls;
    const { request, loading } = useHttp();
    const [groups, setGroups] = useState<Group[]>([]);

    const fetchGroups = async () => {
        const res = await request(groupsUrl);

        setGroups(res);
    };

    return {
        groupsLoading: loading,
        groups,
        fetchGroups
    };
};

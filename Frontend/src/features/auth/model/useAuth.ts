import { useEffect } from "react";
import { useJwt } from "react-jwt";
import { useCookies } from "react-cookie";
import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";
import { useAppDispatch } from "../../../App/appStore.ts";
import { userSlice } from "./authSlice.ts";
export type authReqType = { username: string; password: string };
export const useAuth = () => {
    const { signInUrl } = urls;
    const [cookies, setCookies, removeCookies] = useCookies(["token"]);
    const dispatch = useAppDispatch();

    const { request, loading } = useHttp();
    const { decodedToken, isExpired, reEvaluateToken } = useJwt(cookies.token);

    const signIn = async (body: authReqType) => {
        const response = await request(signInUrl, HTTP_METHOD.POST, body);

        if (!response.err) {
            setCookies("token", response.token);
        }
    };
    useEffect(() => {
        reEvaluateToken(cookies.token);
    }, [cookies]);

    useEffect(() => {
        if (decodedToken) {
            const role =
                // eslint-disable-next-line @typescript-eslint/ban-ts-comment
                // @ts-expect-error
                decodedToken[
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                ];

            // eslint-disable-next-line @typescript-eslint/ban-ts-comment
            // @ts-expect-error
            const id = decodedToken["Id"];
            dispatch(
                userSlice.actions.login({ userId: parseInt(id), role: role })
            );
        } else {
            dispatch(userSlice.actions.logout());
        }
    }, [decodedToken]);

    useEffect(() => {
        if (isExpired) {
            removeCookies("token");
        }
    }, [isExpired]);

    return { loading, signIn };
};

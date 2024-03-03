import { useEffect } from "react";
import { useJwt } from "react-jwt";
import { useCookies } from "react-cookie";
import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";
import { useAppDispatch } from "../../../App/appStore.ts";
import { userSlice } from "./authSlice.ts";
export type authReqType = { username: string; password: string };
export const useAuth = () => {
    const { signInUrl, signUpUrl } = urls;
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
            const name =
                // @ts-ignore
                decodedToken[
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
                ];
            // @ts-ignore
            const userId = decodedToken["Id"];
            dispatch(userSlice.actions.login({ userId: parseInt(userId), userName: name }));
        } else {
            dispatch(userSlice.actions.logout());
        }
    }, [decodedToken]);

    useEffect(() => {
        if (isExpired) {
            removeCookies("token");
        }
    }, [isExpired]);

    const signUp = async (body: authReqType) => {
        const response = await request(signUpUrl, HTTP_METHOD.POST, body);
        if (!response.err) {
            setCookies("token", response.token);
        }
    };
    return { loading, signIn, signUp };
};

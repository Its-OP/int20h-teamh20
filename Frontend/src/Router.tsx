import { Navigate, Route, Routes } from "react-router-dom";
import Auth from "./pages/Auth/ui/Auth.tsx";
import AuthLayout from "./App/ui/AuthLayout.tsx";
import { useAppSelector } from "./App/appStore.ts";

export const Router = () => {
    const { userId } = useAppSelector(state => state.user);
    return (
        <Routes>
            {userId && <Route path={"/"} element={<AuthLayout />}></Route>}

            <Route path={"/auth"} element={<Auth />} />
            <Route path={"/*"} element={<Navigate to={"/auth"} />} />
        </Routes>
    );
};

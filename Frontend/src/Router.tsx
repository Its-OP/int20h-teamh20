import { Navigate, Route, Routes } from "react-router-dom";
import Auth from "./pages/Auth/ui/Auth.tsx";
import AuthLayout from "./App/ui/AuthLayout.tsx";
import { useAppSelector } from "./App/appStore.ts";
import { Students } from "./pages/students";
import { PersonalPage } from "./pages/personalPage";
import { Admin } from "./pages/Admin";

export const Router = () => {
    const { userId } = useAppSelector(state => state.user);
    return (
        <Routes>
            {userId ? (
                <Route path={"/"} element={<AuthLayout />}>
                    <Route path={"/students"} element={<Students />} />
                    <Route path={"/student/:id"} element={<PersonalPage />} />
                    <Route path={"/admin"} element={<Admin />} />
                    <Route
                        path={"/*"}
                        element={<Navigate to={"/students"} />}
                    />
                </Route>
            ) : (
                <>
                    <Route path={"/auth"} element={<Auth />} />
                    <Route path={"/*"} element={<Navigate to={"/auth"} />} />
                </>
            )}
        </Routes>
    );
};

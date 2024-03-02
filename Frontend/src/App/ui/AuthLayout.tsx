import { Layout } from "antd";
import { Outlet } from "react-router-dom";
import { HeaderApp } from "../../widgets/Header";
import { Helmet } from "react-helmet";
import { BASE_TITLE } from "../../shared/model/const.ts";

const AuthLayout = () => {
    return (
        <>
            <Helmet>
                <title>{BASE_TITLE}</title>
            </Helmet>
            <HeaderApp />
            <Layout.Content
                style={{
                    maxWidth: 1280,
                    margin: "auto",
                    marginTop: 40,
                    width: "100%"
                }}
            >
                <Outlet />
            </Layout.Content>
        </>
    );
};
export default AuthLayout;

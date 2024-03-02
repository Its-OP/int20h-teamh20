import { Layout } from "antd";
import { Outlet } from "react-router-dom";

const AuthLayout = () => {
    return (
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
    );
};
export default AuthLayout;

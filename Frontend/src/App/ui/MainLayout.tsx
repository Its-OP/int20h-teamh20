import { FC } from "react";
import { App, Layout } from "antd";
import { Router } from "../../Router.tsx";
import { Helmet } from "react-helmet";
import { BASE_TITLE } from "../../shared/model/const.ts";

const MainLayout: FC = () => {
    return (
        <>
            <Helmet>{BASE_TITLE}</Helmet>
            <App message={{ duration: 5, maxCount: 3 }}>
                <Layout style={{ height: "100vh" }}>
                    <Router />
                </Layout>
            </App>
        </>
    );
};

export default MainLayout;

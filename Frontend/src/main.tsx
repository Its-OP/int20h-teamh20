import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import UA from "antd/locale/uk_UA";
import { ConfigProvider, ConfigProviderProps } from "antd";
import MainLayout from "./App/ui/MainLayout.tsx";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./App/appStore.ts";

const theme: ConfigProviderProps = {
    theme: {
        token: {
            colorPrimary: "#6636c7"
        }
    }
};

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <Provider store={store}>
            <ConfigProvider theme={theme.theme} locale={UA}>
                <BrowserRouter>
                    <MainLayout />
                </BrowserRouter>
            </ConfigProvider>
        </Provider>
    </React.StrictMode>
);

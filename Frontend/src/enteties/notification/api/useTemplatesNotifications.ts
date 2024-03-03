import { useState } from "react";
import { TemplateNotification } from "../../../shared/model/types.ts";
import { useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";
import { message } from "antd";
import { errNotificationMessage } from "../../../shared/model/const.ts";

export const useTemplatesNotification = () => {
    const { templatesMessages } = urls;
    const { request, loading } = useHttp();
    const [templatesNotification, setTemplateNotifications] = useState<
        TemplateNotification[]
    >([]);

    const fetchTemplatesNotification = async () => {
        const res = await request(templatesMessages);

        if (res) {
            setTemplateNotifications(res);
        } else {
            message.error(errNotificationMessage);
        }
    };

    return {
        fetchTemplatesNotification,
        templatesNotification,
        templateNotificationLoading: loading
    };
};

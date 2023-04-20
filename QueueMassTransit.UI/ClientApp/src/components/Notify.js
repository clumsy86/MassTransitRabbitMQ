import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { Button, Input, message, notification } from "antd";
import React, { useEffect, useState } from "react";

export const Notify = () => {
  const [connection, setConnection] = useState(null);
  const [inputText, setInputText] = useState("");

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("https://localhost:58236/hubs/notifications")
      .withAutomaticReconnect()
      .build();

    setConnection(connect);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          connection.on("ReceiveMessage", (message) => {
            notification.open({
              message: message.text,
              text: message,
            });
          });
        })
        .catch((error) => console.log(error));
    }
  }, [connection]);

  return (
    <>
    </>
  );
};
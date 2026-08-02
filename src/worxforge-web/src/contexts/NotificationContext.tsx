import { createContext, useContext } from "react";
import type { PropsWithChildren } from "react";

type NotificationContextValue = {
  unreadCount: number;
};

const NotificationContext = createContext<NotificationContextValue>({ unreadCount: 0 });

export function NotificationProvider({ children }: PropsWithChildren) {
  // TODO: Implementation
  return <NotificationContext.Provider value={{ unreadCount: 0 }}>{children}</NotificationContext.Provider>;
}

export function useNotificationContext(): NotificationContextValue {
  // TODO: Implementation
  return useContext(NotificationContext);
}


import * as React from "react";
import { Header } from "../components/organisms/Header";
import classNames from 'classnames';
import "./usertemplate.css";

interface UserTemplateProps {
  full?: boolean;
}

export const UserTemplate: React.FC<
  React.PropsWithChildren<UserTemplateProps>
> = ({ children, full }) => {
  return (
    <main className="user-template">
      <Header />
      <div className={classNames("container", !full && "adjust")}>{children}</div>
    </main>
  );
};

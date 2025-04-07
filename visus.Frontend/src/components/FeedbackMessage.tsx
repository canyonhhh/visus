import React from "react";
import { CheckCircle, AlertCircle } from "lucide-react";
import clsx from "clsx"; // Optional: for cleaner conditional classNames

interface FeedbackMessageProps {
    type: "success" | "error";
    message: string;
}

const FeedbackMessage: React.FC<FeedbackMessageProps> = ({ type, message }) => {
    return (
        <div
            className={clsx(
                "flex items-center p-4 rounded-md text-sm font-medium shadow-md",
                {
                    "bg-green-100 text-green-800": type === "success",
                    "bg-red-100 text-red-800": type === "error",
                }
            )}
        >
            {type === "success" ? (
                <CheckCircle className="mr-2" size={20} />
            ) : (
                <AlertCircle className="mr-2" size={20} />
            )}
            {message}
        </div>
    );
};

export default FeedbackMessage;

import React, {useRef} from "react";

type ButtonProps = {
    label: string;
    onFileSelect: (file: File | null) => void;
};

const ImportButton: React.FC<ButtonProps> = ({ label, onFileSelect }) => {

    const fileInputRef = useRef<HTMLInputElement>(null);

    const handleClick = () => {
        if (fileInputRef.current) {
            fileInputRef.current.click();
        }
    };

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files ? event.target.files[0] : null;
        onFileSelect(file);
    }

    return (
        <button
            className="flex items-center bg-orange-500 text-white px-4 py-2 rounded-xl cursor-pointer font-medium hover:bg-orange-600 gap-2"
            onClick={handleClick}>
            <svg
                width="24"
                height="24"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
            >
                <path
                    d="M4 16L4 17C4 18.6569 5.34315 20 7 20L17 20C18.6569 20 20 18.6569 20 17L20 16M16 8L12 4M12 4L8 8M12 4L12 16"
                    stroke="#FFFFFF"
                    strokeWidth="1.5"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                />
            </svg>
            <span className="hidden sm:inline">{label}</span>
            <input
                ref={fileInputRef}
                type="file"
                className="hidden"
                accept=".json"
                onChange={handleFileChange}
            />
        </button>
    )
        ;
};

export default ImportButton;

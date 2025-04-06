import ImportButton from "./Components/ImportButton.tsx";
import {useState} from "react";
import AutoForm from "./Components/AutoForm.tsx";

function App() {
    const [selectedFile, setSelectedFile] = useState<File | null>(null);

    const handleFileSelect = (file: File | null) => {
        setSelectedFile(file);
    };

    return (
        <div className="min-h-screen bg-gray-100 text-black flex flex-col">
            {/* Header с кнопкой */}
            <header className="w-full bg-white border-b border-gray-100 p-4 flex justify-start">
                <ImportButton label="Сгенерировать форму" onFileSelect={handleFileSelect}/>
            </header>

            <main className="flex-1 p-6 flex items-center justify-center">
                <AutoForm file={selectedFile}/>
            </main>
        </div>
    );
}

export default App;


import React, {FormEvent, ReactNode, useEffect, useState} from "react";
import {FormBuilder} from "../Utils/FormBuilder.tsx";
import {IFormData} from "../Core/IForm.ts";

type FormComponentProps = {
    file: File | null;
};

const AutoForm: React.FC<FormComponentProps> = ({ file }) => {

    const [formInner, setFormInner] = useState<ReactNode[] | null>(null);
    const [isNotificationVisible, setNotificationVisible] = useState(false);
    const [formData, setFormData] = useState<IFormData | null>(null);

    const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        setNotificationVisible(true);

        setTimeout(() => {
            setNotificationVisible(false);
        }, 3000);
    };

    useEffect(() => {
        if (!file) return;

        const reader = new FileReader();

        reader.onload = function(e) {
            const result = e.target?.result;
            if (!result) return;

            let json: IFormData;
            try {
                json = JSON.parse(result as string);
                setFormData(json);
            } catch (error) {
                console.error("Ошибка при парсинге JSON:", error);
                return;
            }

            const builder = new FormBuilder();

            json.form.items.forEach(item => {
                switch (item.type) {
                    case "button":
                        builder.addButton(item.text!, item.class!);
                        break;
                    case "filler":
                        builder.addFiller(item.message!);
                        break;
                    case "text":
                        builder.addTextInput(item);
                        break;
                    case "select":
                        builder.addSelect(item);
                        break;
                    case "checkbox":
                        builder.addCheckbox(item);
                        break;
                    case "radio":
                        builder.addRadio(item);
                        break;
                    case "textarea":
                        builder.addTextAreaInput(item);
                        break;
                }
            });

            setFormInner(builder.buildForm());
        };

        reader.readAsText(file);
    }, [file]);


    return (
        <form id="dynamicForm"
              className="bg-white shadow-md rounded-xl px-12 pt-8 pb-8 mb-6 w-full max-w-150" onSubmit={handleSubmit}>
            {formInner ? (
                formInner
            ) : (
                <div className="text-gray-500 text-center font-medium">
                    Форма пуста
                </div>
            )}
            {isNotificationVisible && (
                <div className="fixed top-5 left-1/2 transform -translate-x-1/2 bg-orange-500 text-white font-medium p-4 rounded-lg shadow-lg z-50" dangerouslySetInnerHTML={{ __html:  formData?.form.postmessage!}}>
                </div>
            )}
        </form>

    );
};

export default AutoForm;

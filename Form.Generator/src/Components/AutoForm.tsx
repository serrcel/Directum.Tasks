import React from "react";
import {FormBuilder} from "../Utils/FormBuilder.tsx";
import {IForm, IFormData} from "../Core/IForm.ts";

type FormComponentProps = {
    file: File | null;
};

const AutoForm: React.FC<FormComponentProps> = ({ file }) => {

    const componentBuilder = new FormBuilder();

    if (file) {
        const reader = new FileReader();
        reader.readAsText(file!);

        let innerJsonStructure: IForm;

        reader.onload = function(e) {
            innerJsonStructure = (JSON.parse(e.target!.result as string) as IFormData).form;

            componentBuilder.addTitle(innerJsonStructure.name);
        };

    }


    const formInner =
        new FormBuilder()
            .addTitle("Пельмени")
            .addFiller("<br>")
            .addRadio({
                type: 'radio',
                name: 'radio',
                placeholder: "",
                required: true,
                disabled: false,
                class: "",
                items: [
                    {
                        value: "1",
                        label: "Чай",
                        checked: true
                    },
                    {
                        value: "2",
                        label: "Кофе",
                        checked: false
                    },
                    {
                        value: "0",
                        label: "Вода",
                        checked: false
                    }
                ]
            })
            .addTextInput({
                type: 'text',
                name: 'tel',
                placeholder: "dsfsdf",
                label: "Телефон",
                required: true,
                disabled: false,
                validationRules: {
                    type: "tel"
                },
                class: "",
                items: [
                    {
                        value: "1",
                        label: "Чай",
                        checked: true
                    },
                    {
                        value: "2",
                        label: "Кофе",
                        checked: false
                    },
                    {
                        value: "0",
                        label: "Вода",
                        checked: false
                    }
                ]
            })
            .addSelect({
                type: "select",
                name: "select",
                placeholder: "",
                required: true,
                validationRules: {
                    type: "select"
                },
                label: "Должность:",
                class: "",
                disabled: false,
                options: [
                    {
                        value: "1",
                        text: "Руководитель",
                        selected: false
                    },
                    {
                        value: "2",
                        text: "Аналитик",
                        selected: false
                    },
                    {
                        value: "3",
                        text: "ИТ-специалист",
                        selected: false
                    },
                    {
                        value: "25",
                        text: "ИТ-руководитель",
                        selected: false
                    },
                    {
                        value: "125",
                        text: "Пользователь",
                        selected: false
                    }
                ]
            })
            .addButton("Отправить", " ")
            .addCheckbox({
                type: "checkbox",
                name: "checkbox",
                checked: false,
                placeholder: "",
                required: true,
                validationRules: {
                    type: "checkbox"
                },
                label: "Заказать демонстрацию",
                class: "",
                disabled: false
            })
            .addCheckbox({
                type: "checkbox",
                name: "checkbox",
                checked: false,
                placeholder: "",
                required: true,
                validationRules: {
                    type: "checkbox"
                },
                label: "Пернуть",
                class: "",
                disabled: false
            })
            .addTextAreaInput({type: 'text',
                name: 'tel',
                placeholder: "dsfsdf",
                label: "Телефон",
                required: true,
                disabled: false,
                validationRules: {
                    type: "tel"
                },
                class: "",
                items: [
                    {
                        value: "1",
                        label: "Чай",
                        checked: true
                    },
                    {
                        value: "2",
                        label: "Кофе",
                        checked: false
                    },
                    {
                        value: "0",
                        label: "Вода",
                        checked: false
                    }
                ]
            })
            .buildForm()

    return (
        <form id="dynamicForm"
              className="bg-white shadow-md rounded-xl px-12 pt-8 pb-12 mb-6 w-full max-w-[50vw]">
            {formInner}
        </form>

    );
};

export default AutoForm;

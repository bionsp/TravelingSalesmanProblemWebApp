import 'bootstrap/dist/css/bootstrap.css';
import './extra.css';
import React, { useState, useRef } from 'react';
import OperationRequestForm from './OperationRequestForm';

function Solver() {
    const [textareaContent, setTextareaContent] = useState('');
    const textareaRef = useRef(null);
    const borderShownRef = useRef(null);


    const copyButtonClick = () => {
        if (borderShownRef.current) {
            clearTimeout(borderShownRef.current);
        }
        navigator.clipboard.writeText(textareaContent);
        textareaRef.current.classList.add('bg-light', 'bg-success-fade');
        borderShownRef.current = setTimeout(() => {
            textareaRef.current.classList.remove('bg-light');
            setTimeout(() => {
                textareaRef.current.classList.remove('bg-success-fade');
            }, 200);
        }, 200);
    };

    return (
        <div className="container">
            <div className="row vh-100 justify-content-center align-items-center">
                <div className="col-lg-6">
                    <OperationRequestForm textareaContent={textareaContent} setTextareaContent={setTextareaContent} />
                    <div className='mb-3'>
                        <textarea ref={textareaRef} value={textareaContent} className='form-control' placeholder='Output' style={{ height: 256 + 'px', resize: 'none' }} disabled></textarea>
                    </div>
                    <div className='mb-3'>
                        <button type='button' onClick={copyButtonClick} className='btn btn-secondary shadow-none'>Copy result</button> 
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Solver;
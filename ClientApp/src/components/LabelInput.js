import 'bootstrap/dist/css/bootstrap.css';

function LabelInput({ labelName, min, max, step, value, onChange }) {
    return (
        <>
            <div className="row mb-3">
                <div className="col-12">
			        <label htmlFor={labelName} className="">{labelName}</label>
                </div>
            </div>
            <div className="row mb-3">
                <div className="col-12">
                    <input id={labelName} type="number" className="form-control" min={min} max={max} step={step} value={value} onChange={onChange} required />
                </div>
            </div>
        </>
    );
}

export default LabelInput;
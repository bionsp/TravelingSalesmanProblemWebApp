import React, { useState, useRef } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.css';
import LabelInput from './LabelInput';

function OperationRequestForm({ textareaContent, setTextareaContent }) {
	const [epochCount, setEpochCount] = useState('');
    const [tournamentSize, setTournamentSize] = useState('');
    const [populationSize, setPopulationSize] = useState('');
    const [crossoverScale, setCrossoverScale] = useState('');
    const [mutationScale, setMutationScale] = useState('');
	const [warningExists, setWarningExists] = useState(false);
	const [warningReason, setWarningReason] = useState('');
	const [rerender, setRerender] = useState(false);
	const [isSubmitEnabled, setIsSubmitEnabled] = useState(false);
	const fileInputRef = useRef(null);
	const checkFormat = async () => {
		setWarningExists(false);
		const reader = new FileReader();
		const file = fileInputRef.current.files[0];
		if (file) {
			reader.onload = async (event) => {
				let warning = false;
				if (event.target.result == null) {
					return;
				}
				const txt = event.target.result;
				var line = txt.split('\n');
				const serverUrl = process.env.REACT_APP_SERVER_URL;
				try {
					const response = await axios.get(serverUrl + '/max-line-count');
					if (line.length > response.data) {
						warning = true;
						setWarningReason('Too many lines. You are allowed to have at most ' + response.data + ' lines in the text file.');
					}
					for (var i = 0; i < line.length && !warning; i++) {
						var str = line[i].replace(/\s{2,}/g, ' ').trim();
						var num = str.split(' ');
						if (num.length !== 2) {
							warning = true;
							setWarningReason('Wrong format. You are supposed to have two numbers separated by a space on each line.');
						}
						const num1 = Number(num[0]);
						const num2 = Number(num[1]);
						if (!(!isNaN(num1) && typeof num1 == 'number' && !isNaN(num2) && typeof num2 == 'number')) {
							warning = true;
							setWarningReason('Wrong format. You are supposed to have two numbers separated by a space on each line.');
						}
					}
					if (warning) {
						fileInputRef.current.value = '';
						setWarningExists(true);
						setIsSubmitEnabled(false);
					}
					else {
						setIsSubmitEnabled(true);
					}
					setRerender(!rerender);
				} catch (error) {
					console.error(error);
				}
			}
			reader.readAsText(file);
		}
	};
	const handleFormSubmit = async (e) => {
		e.preventDefault();
		if (fileInputRef.current.files.length === 0) {
			alert('File not uploaded');
			return;
		}
		const formData = new FormData();
		formData.append('TextFile', fileInputRef.current.files[0]);
		formData.append('TspParameters.EpochCount', epochCount.toString());
		formData.append('TspParameters.PopulationSize', populationSize.toString());
		formData.append('TspParameters.TournamentSize', tournamentSize.toString());
		formData.append('TspParameters.CrossoverScale', crossoverScale.toString());
		formData.append('TspParameters.MutationScale', mutationScale.toString());
		const serverUrl = process.env.REACT_APP_SERVER_URL;
		try {
			const response = await axios.post(serverUrl + '/genetic-algorithm-computation-request', formData, {
				headers: {
					'Content-Type': 'multipart/form-data',
				},
			});
			if (response.data.success) {
				const answer = response.data.data;
				setTextareaContent(answer);
			}
		} catch (error) {
			console.error(error);
		}
	};
    return (
		<form onSubmit={handleFormSubmit}>
			<div className="alert alert-primary mt-3" role="alert">
				You are expected to upload a text file which contains two numbers separated by a space on each line. The text file can have at most 2000 lines.
			</div>
			{ warningExists &&
				<div className="mt-3 alert alert-warning alert-dismissible fade show" role="alert">
					{ warningReason }
					<button onClick={() => setWarningExists(false)} type="button" className="btn-close shadow-none" data-bs-dismiss="alert" aria-label="Close"></button>
				</div>
			}
			<div className='mb-3 mt-3'>
				<input onChange={checkFormat} ref={fileInputRef} type='file' className='form-control' />
			</div>
			<LabelInput labelName='Epoch count:' min='1' max='100' value={epochCount} onChange={(e) => setEpochCount(e.target.value)} />
			<LabelInput labelName='Population size:' min='5' max='1000' value={populationSize} onChange={(e) => setPopulationSize(e.target.value)} />
			<LabelInput labelName='Tournament size:' min='1' max='20' value={tournamentSize} onChange={(e) => setTournamentSize(e.target.value)} />
			<LabelInput labelName='Crossover scale:' min='0' max='1' step="0.01" value={crossoverScale} onChange={(e) => setCrossoverScale(e.target.value)} />
			<LabelInput labelName='Mutation scale:' min='0' max='1' step="0.01" value={mutationScale} onChange={(e) => setMutationScale(e.target.value)} />
			<div className='mb-3'>
				<button type='submit' className={`btn btn-primary shadow-none ${isSubmitEnabled ? '' : 'disabled'}`}>Submit</button>
			</div>

		</form>
    );
}

export default OperationRequestForm;
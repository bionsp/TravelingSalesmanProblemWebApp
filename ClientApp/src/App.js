import Layout from './components/Layout';
import OperationRequestForm from './components/OperationRequestForm';
import Home from './components/Home';
import Solver from './components/Solver';
import { BrowserRouter, Routes, Route } from "react-router-dom";

function App() {
	return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Home />} />
                    <Route path="solver" element={<Solver />} />
                </Route>
            </Routes>
        </BrowserRouter>
	);
}

export default App;

import 'bootstrap/dist/css/bootstrap.css';

function Home() {
    return (
        <div className="container-xxl d-flex align-items-center justify-content-center vh-100">
            <div>
                <h1 className="text-center">Welcome to TSP Solver!</h1>
                <p className="text-center">You can use this website for solving a Traveling Salesman Problem with your own input data.</p>
            </div>
        </div>
    );
}

export default Home;
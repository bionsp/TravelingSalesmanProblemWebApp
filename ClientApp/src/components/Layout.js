import { Outlet, NavLink } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.css';

const Layout = () => {
    return (
        <>
            <nav className="navbar navbar-expand-sm bg-primary navbar-dark">
                <div className="container-xl">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <NavLink className="nav-link" activeclassname="active" to="/">Home</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" activeclassname="active" to="/solver">Solver</NavLink>
                        </li>
                    </ul>
                </div>
            </nav>
            <Outlet />
        </>
    )
};

export default Layout;
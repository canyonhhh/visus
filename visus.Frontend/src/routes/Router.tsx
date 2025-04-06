import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import PublicPage from "../pages/PublicPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import ProtectedRoute from "./ProtectedRoute"
import HomePage from "../pages/HomePage"
import AnalyticsPage from "../pages/AnalyticsPage"
import ParticipantsPage from "../pages/ParticipantsPage"
import QrcodePage from "../pages/QrcodePage"
import StatisticsPage from "../pages/StatisticsPage"
import SecretPage from "../pages/SecretPage"
const AppRouter = () => {
    return (
        <Router>
            <div >
                <Routes>
                    <Route path="/" element={<PublicPage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route element={<ProtectedRoute />}>
                        <Route path="/home" element={<HomePage />} />
                        <Route path="/analytics" element={<AnalyticsPage />} />
                        <Route path="/participants" element={<ParticipantsPage />} />
                        <Route path="/qrcode" element={<QrcodePage />} />
                        <Route path="/statistics" element={<StatisticsPage />} />
                    </Route>
                    <Route element={<ProtectedRoute requiredRole="SYSTEM" />}>
                        <Route path="/secret" element={<SecretPage />} />
                    </Route>
                </Routes>
            </div>
        </Router>
    );
};

export default AppRouter;

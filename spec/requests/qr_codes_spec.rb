require 'rails_helper'

RSpec.describe "/qr_codes", type: :request do

  let!(:business) { Business.create!(name: "Test Business", contact_info: "+37060000000") }
  let!(:activity) { Activity.create!(name: "Test Activity", description: "Activity Description", is_active: true, business: business) }

  let(:valid_attributes) { { token: 'token123abc', is_active: true, activity_id: activity.id } }

  let(:invalid_attributes) {
    { token: nil, is_active: nil, activity_id: nil }
  }

  describe "GET /index" do
    it "renders a successful response" do
      QrCode.create! valid_attributes
      get qr_codes_url
      expect(response).to be_successful
    end
  end

  describe "GET /show" do
    it "renders a successful response" do
      qr_code = QrCode.create! valid_attributes
      get qr_code_url(qr_code)
      expect(response).to be_successful
    end
  end

  describe "GET /new" do
    it "renders a successful response" do
      get new_qr_code_url
      expect(response).to be_successful
    end
  end

  describe "GET /edit" do
    it "renders a successful response" do
      qr_code = QrCode.create! valid_attributes
      get edit_qr_code_url(qr_code)
      expect(response).to be_successful
    end
  end

  describe "POST /create" do
    context "with valid parameters" do
      it "creates a new QrCode" do
        expect {
          post qr_codes_url, params: { qr_code: valid_attributes }
        }.to change(QrCode, :count).by(1)
      end

      it "redirects to the created qr_code" do
        post qr_codes_url, params: { qr_code: valid_attributes }
        expect(response).to redirect_to(qr_code_url(QrCode.last))
      end
    end

    context "with invalid parameters" do
      it "does not create a new QrCode" do
        expect {
          post qr_codes_url, params: { qr_code: invalid_attributes }
        }.to change(QrCode, :count).by(0)
      end

      it "renders a response with 422 status (i.e. to display the 'new' template)" do
        post qr_codes_url, params: { qr_code: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "DELETE /destroy" do
    it "destroys the requested qr_code" do
      qr_code = QrCode.create! valid_attributes
      expect {
        delete qr_code_url(qr_code)
      }.to change(QrCode, :count).by(-1)
    end

    it "redirects to the qr_codes list" do
      qr_code = QrCode.create! valid_attributes
      delete qr_code_url(qr_code)
      expect(response).to redirect_to(qr_codes_url)
    end
  end
end
